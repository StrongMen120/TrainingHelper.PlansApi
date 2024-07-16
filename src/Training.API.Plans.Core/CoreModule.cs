using Training.Common.Hexagon.Core.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Training.API.Plans.Core.UseCases;
using Training.API.Plans.Core.Domain.FakeData;

namespace Training.API.Plans;

public sealed class CoreModule : ModuleServicesRegistry
{
    public CoreModule()
    {
        this.AddServices("UseCases", this.RegisterUseCases);
    }

    private void RegisterUseCases(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        services.AddScoped<TestEmployerFake>();

        services.AddScoped<GetAllExercisesInfoUseCase>();
        services.AddScoped<GetExerciseInfoUseCase>();
        services.AddScoped<CreateExerciseInfoUseCase>();
        services.AddScoped<DeleteExerciseInfoUseCase>();
        services.AddScoped<UpdateExerciseInfoUseCase>();
        
        services.AddScoped<GetExercisesAllRecordsUseCase>();
        services.AddScoped<GetExercisesRecordsHistoryUseCase>();
        services.AddScoped<GetExercisesStatisticsUseCase>();
        services.AddScoped<CreateExercisesRecordsUseCase>();

        services.AddScoped<UpdateAttachmentExerciseInfoUseCase>();
        services.AddScoped<GetAttachmentExerciseInfoUseCase>();
        services.AddScoped<DeleteAttachmentExerciseInfoUseCase>();
        
        services.AddScoped<DeleteDoneExerciseUseCase>();
        services.AddScoped<CreateDoneExerciseUseCase>();
        services.AddScoped<GetAllDoneExerciseUseCase>();
        services.AddScoped<UpdateDoneExerciseUseCase>();

        services.AddScoped<CreatePlansUseCase>();
        services.AddScoped<DeletePlansUseCase>();
        services.AddScoped<GetAllUserPlansUseCase>();
        services.AddScoped<GetPlansUseCase>();
        services.AddScoped<UpdatePlansUseCase>();        

        services.AddScoped<CreatePlannedTrainingUseCase>();
        services.AddScoped<DeletePlannedTrainingUseCase>();
        services.AddScoped<GetAllUserPlannedTrainingUseCase>();
        services.AddScoped<GetPlannedTrainingUseCase>();
        services.AddScoped<UpdatePlannedTrainingUseCase>();

        services.AddScoped<DeleteTestEmployersUseCase>();
        services.AddScoped<CreateTestEmployersUseCase>();
        services.AddScoped<GetTestEmployersUseCase>();
        services.AddScoped<UpdateTestEmployersUseCase>();
    }
}
