using System.Reflection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Serilog;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Persistance;
using Training.API.Plans.Persistance.Mappings;
using Training.API.Plans.Persistance.Repositories;
using Training.API.Plans.Persistance.TrainingsDb;
using Training.Common.Configuration;
using Training.Common.Hexagon.Core.Infrastructure;
using Training.Common.Hexagon.Persistance;

namespace Training.API.Plans;

public sealed class PersistanceModule : ModuleServicesRegistry
{
    public PersistanceModule()
    {
        this.AddServices("Repositories", this.RegisterRepositories);
        this.AddServices("Databases", this.RegisterDatabases);
        this.AddServices("Mappings", this.RegisterMappings);
        this.AddServicesInitializer("Databases", this.InitializeDatabases);
    }

    private void RegisterMappings(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        logger.Information("Registering Persistance mappers...");
        services.AddSingleton<IPersistanceMapper>((_) => {
            var fork = TypeAdapterConfig.GlobalSettings.Fork(f =>
            {
                f.Apply(new EntityMappingRegistry(),
                    new PersistanceMappingRegistry(),
                    new NodaTimeMappingsRegister());
                
                f.RequireExplicitMapping = false;
                f.RequireDestinationMemberSource = false;
            });

            fork.Compile();

            return new PersistanceMapper(fork);
        });
        logger.Debug("Registering Persistance mappers... DONE");
    }

    private void RegisterRepositories(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        services.AddScoped<IExercisesInfoRepository, ExercisesInfoRepository>();
        services.AddScoped<IExercisesRecordsRepository, ExercisesRecordsRepository>();
        services.AddScoped<IDoneExercisesRepository, DoneExercisesRepository>();
        services.AddScoped<IPlannedExercisesRepository, PlannedExercisesRepository>();
        services.AddScoped<IPlansRepository, PlansRepository>();
        services.AddScoped<IPlannedTrainingsRepository, PlannedTrainingsRepository>();
        services.AddScoped<ITestEmployersRepository, TestEmployersRepository>();
    }

    private void RegisterDatabases(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        var trainingsDbConfig = configuration.GetRequiredSection<PostgresDbConfiguration>(Constants.ConfigSections.Databases.Trainings);

        logger.Information("Loaded training DB configuration {@trainingsDbConfig}", new
        {
            HasConnectionString = !string.IsNullOrWhiteSpace(trainingsDbConfig.ConnectionString),
            trainingsDbConfig.DefaultDatabase,
            trainingsDbConfig.EnableAutomaticMigration,
            trainingsDbConfig.PostgresApiVersion,
        });

        var configureTrainingDb = (DbContextOptionsBuilder builder) =>
        {
            builder.UseNpgsql(trainingsDbConfig.ConnectionString, config =>
            {
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                config.SetPostgresVersion(trainingsDbConfig.PostgresApiVersion);
                config.MigrationsHistoryTable(TrainingDatabaseConstants.MigrationsHistoryTableName, TrainingDatabaseConstants.MigrationsHistoryTableSchema);
                config.UseNetTopologySuite();
                config.UseNodaTime();
            });
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        };

        services.AddDbContext<TrainingDbContext>(configureTrainingDb, ServiceLifetime.Scoped, ServiceLifetime.Singleton);
        services.AddDbContextFactory<TrainingDbContext>(configureTrainingDb);
        services.AddHealthChecks()
            .AddDbContextCheck<TrainingDbContext>("Database");
    }

    private void InitializeDatabases(IServiceProvider servicesProvider, Serilog.ILogger logger)
    {
        var configuration = servicesProvider.GetRequiredService<IConfiguration>();
        var trainingPlansDbConfig = configuration.GetRequiredSection<PostgresDbConfiguration>(Constants.ConfigSections.Databases.Trainings);

        if (trainingPlansDbConfig.EnableAutomaticMigration)
        {
            MigrateDatabase<TrainingDbContext>(servicesProvider, logger);
        }
        else
        {
            logger.Information($"Migration of '{typeof(TrainingDbContext).Name}' disabled (EnableAutomaticMigration is '{trainingPlansDbConfig.EnableAutomaticMigration}').");
        }
    }

    private static void MigrateDatabase<TContext>(IServiceProvider services, Serilog.ILogger logger)
            where TContext : DbContext
    {
        try
        {
            logger.Information($"Migration of '{typeof(TContext).Name}' started.");

            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Database.Migrate();
            
            using (var conn = (NpgsqlConnection)context.Database.GetDbConnection())
            {
                conn.Open();
                conn.ReloadTypes();
            }

            logger.Information($"Migration of '{typeof(TContext).Name}' completed.");
        }
        catch (Exception ex)
        {
            logger.Information(ex, $"Migration of '{typeof(TContext).Name}' failed.");
        }
    } 
}