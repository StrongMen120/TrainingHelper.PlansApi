using System.Reflection;
using HealthChecks.Prometheus.Metrics;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Serilog;
using Training.API.Plans;
using Training.Common.AspNetCore.Utils;
using Training.Common.Configuration;
using Training.Common.Utils;

const string CorsPolicyName = "CORS";

#if DEBUG
var startupConfig = new ConfigurationBuilder().Apply(b => StartupConfigurationHelper.LoadStartupConfiguration(b, true)).Build();
#else
var startupConfig = new ConfigurationBuilder().Apply(b => StartupConfigurationHelper.LoadStartupConfiguration(b, false)).Build();
#endif

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(startupConfig, Constants.ConfigSections.Serilog)
    .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
    .CreateLogger();

var jsonSerializerConfigurator = JsonSerializerSettingsConfigurator.Build((settings) =>
{
    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    settings.Converters.Add(new StringEnumConverter());

    settings.NullValueHandling = NullValueHandling.Ignore;
    settings.TypeNameHandling = TypeNameHandling.None;

    // Configure NodaTime
    settings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    settings.Converters.Remove(NodaConverters.LocalDateConverter);
    settings.Converters.Add(new ExtendedLocalDateConverter());
});

WebApplication BuildWebApplication()
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddConfiguration(startupConfig);

    builder.Logging.ClearProviders();
    builder.Host.UseSerilog(Log.Logger, true);

    builder.Services.AddAuthorization(o => o.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .Build())
        .AddAuthentication(options => builder.Configuration.Bind(Constants.ConfigSections.Authentication.DefaultConfig, options))
        .AddJwtBearer(options => builder.Configuration.Bind(Constants.ConfigSections.Authentication.JwtBearer, options));

    builder.Services.AddJsonSerializerConfigurator(jsonSerializerConfigurator, true);
    builder.Services.AddCors(options => options.AddPolicy(CorsPolicyName, builder.Configuration.GetSection<CorsPolicy>()));
    // builder.Services.AddCors(options =>
    // {
    //     Log.Warning("Setting cors policy to allow any");
    //     options.AddDefaultPolicy(builder => builder
    //         .AllowAnyHeader()
    //         .AllowAnyMethod()
    //         .AllowAnyOrigin()
    //         .Build()
    //     );
    //     var policy = options.GetPolicy(options.DefaultPolicyName);
    //     Log.Information("Default cors policy is set to {@CorsPolicy}", policy);
    // });

    builder.Services.AddHealthChecks()
        .AddCheck("Self", () => HealthCheckResult.Healthy());

    // Add services to the container.
    builder.Services.AddControllers().AddNewtonsoftJson(options => jsonSerializerConfigurator.ApplyTo(options.SerializerSettings));
    builder.Services.AddHttpContextAccessor();
    // Register application components
    builder.AddModule<CoreModule>();
    builder.AddModule<ApiModule>();
    builder.AddModule<IntegrationsModule>();
    builder.AddModule<PersistanceModule>();

    var app = builder.Build();

    // Initialize application components
    app.InitModule<CoreModule>();
    app.InitModule<ApiModule>();
    app.InitModule<IntegrationsModule>();
    app.InitModule<PersistanceModule>();
    
    app.UseCors(CorsPolicyName);
    app.UseRouting();
    app.UseAuthorization();
    app.UseHttpsRedirection();

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = (_) => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    });
    app.MapHealthChecks("/metrics", new HealthCheckOptions
    {
        ResponseWriter = PrometheusResponseWriter.WritePrometheusResultText,
    });
    app.Map("/version", () => new
    {
        InformationalVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
        Version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyVersionAttribute>()?.Version,
        FileVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version,
    });
    app.MapControllers();

    return app;
}

try
{
    Log.Information("Application initializing...");

    var app = BuildWebApplication();

    Log.Information("Application starting...");

    await app.RunAsync();

    Log.Information("Application terminated safely.");
    Environment.ExitCode = 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
    Environment.ExitCode = 1;
}
finally
{
    Log.CloseAndFlush();
}