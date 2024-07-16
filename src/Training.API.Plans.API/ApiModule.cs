using Training.API.Plans.API;
using Training.Common.Configuration;
using Training.Common.Hexagon.API;
using Training.Common.Hexagon.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Training.API.Plans.API.Configuration;
using Training.API.Plans.API.V1.Mappings;

namespace Training.API.Plans;
public sealed class ApiModule : AppModuleServicesRegistry
{
    public ApiModule()
    {
        this.AddServices("Presenters", this.RegisterPresenters);
        this.AddServices("Mappings", this.RegisterMappings);
        this.AddServices("Swagger", this.RegisterSwagger);
        this.AddServices("ApiVersioning", this.RegisterApiVersioning);
        this.AddAppInitializer("Swagger", this.InitializeSwagger);
    }

    private void RegisterApiVersioning(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        services.AddApiVersioning(o => o.ReportApiVersions = true)
            .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddEndpointsApiExplorer();
    }
    private void RegisterSwagger(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen();
        services.AddSwaggerGenNewtonsoftSupport();
    }

    private void RegisterPresenters(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        services.AddScoped<API.V1.Endpoints.ExercisesInfo.GetAll.GetAllExercisesInfoPresenter>();
        services.AddScoped<API.V1.Endpoints.ExercisesInfo.Get.GetExerciseInfoPresenter>();
        services.AddScoped<API.V1.Endpoints.ExercisesInfo.Create.CreateExerciseInfoPresenter>();
        services.AddScoped<API.V1.Endpoints.ExercisesInfo.Delete.DeleteExerciseInfoPresenter>();
        services.AddScoped<API.V1.Endpoints.ExercisesInfo.Update.UpdateExerciseInfoPresenter>();

        services.AddScoped<API.V1.Endpoints.Attachment.Update.UpdateAttachmentExerciseInfoPresenter>();
        services.AddScoped<API.V1.Endpoints.Attachment.Get.GetAttachmentExerciseInfoPresenter>();
        services.AddScoped<API.V1.Endpoints.Attachment.Delete.DeleteAttachmentExerciseInfoPresenter>();

        
        services.AddScoped<API.V1.Endpoints.DoneExercise.GetAll.GetAllDoneExercisePresenter>();
        services.AddScoped<API.V1.Endpoints.DoneExercise.Create.CreateDoneExercisePresenter>();
        services.AddScoped<API.V1.Endpoints.DoneExercise.Delete.DeleteDoneExercisePresenter>();
        services.AddScoped<API.V1.Endpoints.DoneExercise.Update.UpdateDoneExercisePresenter>();
        
        services.AddScoped<API.V1.Endpoints.Plans.GetAll.GetAllUserPlansPresenter>();
        services.AddScoped<API.V1.Endpoints.Plans.Get.GetPlansPresenter>();
        services.AddScoped<API.V1.Endpoints.Plans.Create.CreatePlansPresenter>();
        services.AddScoped<API.V1.Endpoints.Plans.Delete.DeletePlansPresenter>();
        services.AddScoped<API.V1.Endpoints.Plans.Update.UpdatePlansPresenter>();

        services.AddScoped<API.V1.Endpoints.Statistics.GetAllRecords.GetExercisesAllRecordsPresenter>();
        services.AddScoped<API.V1.Endpoints.Statistics.GetRecordsHistory.GetExercisesRecordsHistoryPresenter>();
        services.AddScoped<API.V1.Endpoints.Statistics.GetExerciseStatistics.GetExerciseStatisticsPresenter>();
        services.AddScoped<API.V1.Endpoints.Statistics.Create.CreateExercisesRecordsPresenter>();
        
        services.AddScoped<API.V1.Endpoints.PlannedTraining.GetAll.GetAllUserPlannedTrainingPresenter>();
        services.AddScoped<API.V1.Endpoints.PlannedTraining.Get.GetPlannedTrainingPresenter>();
        services.AddScoped<API.V1.Endpoints.PlannedTraining.Create.CreatePlannedTrainingPresenter>();
        services.AddScoped<API.V1.Endpoints.PlannedTraining.Delete.DeletePlannedTrainingPresenter>();
        services.AddScoped<API.V1.Endpoints.PlannedTraining.Update.UpdatePlannedTrainingPresenter>();

        
        services.AddScoped<API.V1.Endpoints.TestEmployers.Get.GetTestEmployersPresenter>();
        services.AddScoped<API.V1.Endpoints.TestEmployers.Create.CreateTestEmployersPresenter>();
        services.AddScoped<API.V1.Endpoints.TestEmployers.Delete.DeleteTestEmployersPresenter>();
        services.AddScoped<API.V1.Endpoints.TestEmployers.Update.UpdateTestEmployersPresenter>();
    }

    private void RegisterMappings(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        logger.Information("Registering Api mappers...");
        services.AddSingleton<IApiMapper>((_)=>{
            var fork = TypeAdapterConfig.GlobalSettings.Fork(f =>
            {
                f.Apply(new V1DtosMappingRegister(), new NodaTimeMappingsRegister());

                f.RequireExplicitMapping = false;
                f.RequireDestinationMemberSource = false;
            });

            fork.Compile();

            return new ApiMapper(fork);
        });
        logger.Debug("Registering Api mappers... DONE");
    }
    
    private void InitializeSwagger(IApplicationBuilder app, Serilog.ILogger logger)
    {
        var config = app.ApplicationServices.GetRequiredService<IConfiguration>().GetSection<SwaggerConfiguration>(Constants.ConfigSections.Swagger.SectionName);
        var apiVersionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        if (config == null) return;

        if (config.Enable)
        {
            logger.Information("Enabling swagger documentation");

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = $"swagger";
                options.SwaggerEndpoint($"all/swagger.json", "All versions");
                foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

                options.DisplayOperationId();
            });
        }

        if (config.Enable && config.AutoRedirect)
        {
            logger.Information("Enabling swagger documentation auto-redirect");

            var option = new RewriteOptions();
            option.AddRedirect("^$", $"/swagger");
            app.UseRewriter(option);
        }
    }
}