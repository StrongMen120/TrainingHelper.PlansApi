using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Integrations.Services;
using Training.API.Plans.Integrations.Mappings;
using Training.Common.Configuration;
using Training.Common.Hexagon.Core.Infrastructure;
using Training.Common.Hexagon.Integration;
using Auth0.AuthenticationApi;
using Training.API.Plans.Integrations.Configuration;

namespace Training.API.Plans;

public sealed class IntegrationsModule : ModuleServicesRegistry
{
    public IntegrationsModule()
    {
        this.AddServices("ExternalServices", this.ExternalServices);
        this.AddServices("Services", this.RegisterServicesInternal);
        this.AddServices("Mappings", this.RegisterMappings);
    }

    private void RegisterServicesInternal(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        services.AddScoped<IUserCache, UserCache>();
    }

    private void RegisterMappings(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {  
    }

    private void ExternalServices(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        var usersApiConfig = configuration.GetRequiredSection<OpenApiClientConfiguration>(Constants.ExternalServiceSection.UsersApi);

        services.AddScoped<Training.API.Users.Integration.Api.IUsersApi>((svc) =>
        {
            return new Training.API.Users.Integration.Api.UsersApi(GetConfigurationForUsersAPI(svc, usersApiConfig.BasePath, (int)usersApiConfig.Timeout.TotalMilliseconds));
        });

        services.AddHttpContextAccessor();
        var authenticationApiConfig = configuration.GetRequiredSection<string>(Constants.ExternalServiceSection.AuthenticationApi);
        services.AddScoped<IAuthenticationApiClient>(_ => new AuthenticationApiClient(new Uri(authenticationApiConfig)));
        services.AddScoped<IAuthenticationDetailsProvider, AuthenticationDetailsProvider>();

        var s3StoragConfig = configuration.GetRequiredSection<StorageConfiguration>(Constants.ExternalServiceSection.S3Storage);
        services.AddScoped<IStorageService>(_ => {
            return new StorageService(s3StoragConfig);
        });
    }

    private static Training.API.Users.Integration.Client.Configuration GetConfigurationForUsersAPI(IServiceProvider svc, string basePath, int timeout)
    {
        var httpContext = svc.GetRequiredService<IHttpContextAccessor>().HttpContext;
        string authHeader = httpContext?.Request.Headers["Authorization"] ?? "";
        string cleanJWTToken = authHeader.Replace("Bearer ", "");

        return new Training.API.Users.Integration.Client.Configuration()
        {
            AccessToken = cleanJWTToken,
            BasePath = basePath,
            Timeout = timeout
        };
    }
}