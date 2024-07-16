using Mapster;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Persistance.TrainingsDb.Entities;

namespace Training.API.Plans.Persistance.Mappings;

internal sealed class PersistanceMappingRegistry : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        RegisterCommonMappings(config);

        config.NewConfig<PlansType, Core.Domain.Values.PlansType>()
            .TwoWays()
            .EnumMappingStrategy(EnumMappingStrategy.ByName);

        config.NewConfig<PlansImage, Core.Domain.Values.PlansImage>()
            .TwoWays()
            .EnumMappingStrategy(EnumMappingStrategy.ByName);

        config.NewConfig<ExerciseInfoEntity, ExercisesInfoModel>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.AuthorId, s => s.AuthorId)
            .Map(d => d.BodyElements, s => GetListBodyElementsFromString(s.BodyElements))
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.CreatedAt, s => s.CreatedAt)
            .Map(d => d.CreatedBy, s => s.CreatedBy)
            .Map(d => d.ModifiedAt, s => s.ModifiedAt)
            .Map(d => d.ModifiedBy, s => s.ModifiedBy);
        
        config.NewConfig<PlansEntity, PlansModel>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Image, s => s.Image)
            .Map(d => d.AuthorId, s => s.AuthorId)
            .Map(d => d.PlannedExercise, s => s.PlannedExercises)
            .Map(d => d.CreatedAt, s => s.CreatedAt)
            .Map(d => d.CreatedBy, s => s.CreatedBy)
            .Map(d => d.ModifiedAt, s => s.ModifiedAt)
            .Map(d => d.ModifiedBy, s => s.ModifiedBy);
        
        config.NewConfig<PlannedTrainingsEntity, PlannedTrainingsModel>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.PlansId, s => s.PlansId)
            .Map(d => d.PlansType, s => s.PlansType)
            .Map(d => d.DateStart, s => s.DateStart)
            .Map(d => d.DateEnd, s => s.DateEnd)
            .Map(d => d.UserId, s => s.UserId)
            .Map(d => d.TrainerId, s => s.TrainerId)
            .Map(d => d.GroupId, s => s.GroupId)
            .Map(d => d.Plans, s => s.Plans)
            .Map(d => d.CreatedAt, s => s.CreatedAt)
            .Map(d => d.CreatedBy, s => s.CreatedBy)
            .Map(d => d.ModifiedAt, s => s.ModifiedAt)
            .Map(d => d.ModifiedBy, s => s.ModifiedBy);

        config.NewConfig<PlannedExercisesEntity, PlannedExercisesModel>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.PlansId, s => s.PlansId)
            .Map(d => d.ExerciseInfoId, s => s.ExerciseInfoId)
            .Map(d => d.Series, s => s.Series)
            .Map(d => d.Reps, s => GetListDoubleFromString(s.Reps))
            .Map(d => d.Weight, s => GetListDoubleFromString(s.Weight))
            .Map(d => d.Rate, s => s.Rate)
            .Map(d => d.Rpe, s => s.RPE)
            .Map(d => d.BrakeSeconds, s => s.BrakeSeconds)
            .Map(d => d.Exercise, s => s.ExerciseInfo)
            .Map(d => d.CreatedAt, s => s.CreatedAt)
            .Map(d => d.CreatedBy, s => s.CreatedBy)
            .Map(d => d.ModifiedAt, s => s.ModifiedAt)
            .Map(d => d.ModifiedBy, s => s.ModifiedBy);
        
        config.NewConfig<DoneExercisesEntity, DoneExercisesModel>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.UserId, s => s.UserId)
            .Map(d => d.Date, s => s.Date)
            .Map(d => d.ExerciseInfoId, s => s.ExerciseInfoId)
            .Map(d => d.Series, s => s.Series)
            .Map(d => d.Series, s => s.Series)
            .Map(d => d.Reps, s => GetListDoubleFromString(s.Reps))
            .Map(d => d.Weight, s => GetListDoubleFromString(s.Weight))
            .Map(d => d.Rate, s => s.Rate)
            .Map(d => d.Rpe, s => s.RPE)
            .Map(d => d.BrakeSeconds, s => s.BrakeSeconds)
            .Map(d => d.Exercise, s => s.ExerciseInfo)
            .Map(d => d.CreatedAt, s => s.CreatedAt)
            .Map(d => d.CreatedBy, s => s.CreatedBy)
            .Map(d => d.ModifiedAt, s => s.ModifiedAt)
            .Map(d => d.ModifiedBy, s => s.ModifiedBy);

        config.NewConfig<ExerciseRecordsEntity, ExercisesRecordsModel>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.Revision, s => s.Revision)
            .Map(d => d.ExerciseId, s => s.RegistryEntry.ExerciseId)
            .Map(d => d.UserId, s => s.RegistryEntry.UserId)
            .Map(d => d.Date, s => s.Date)
            .Map(d => d.Reps, s => s.Reps)
            .Map(d => d.Weight, s => s.Weight)
            .Map(d => d.LombardiResult, s => s.LombardiResult)
            .Map(d => d.BrzyckiResult, s => s.BrzyckiResult)
            .Map(d => d.EpleyResult, s => s.EpleyResult)
            .Map(d => d.MayhewResult, s => s.MayhewResult)
            .Map(d => d.AdamsResult, s => s.AdamsResult)
            .Map(d => d.BaechleResult, s => s.BaechleResult)
            .Map(d => d.BergerResult, s => s.BergerResult)
            .Map(d => d.BrownResult, s => s.BrownResult)
            .Map(d => d.OneRepetitionMaximum, s => s.OneRepetitionMaximum)
            .Map(d => d.isAutomat, s => s.isAutomat)
            .Map(d => d.CreatedAt, s => s.RegistryEntry.CreatedAt)
            .Map(d => d.CreatedBy, s => s.RegistryEntry.CreatedBy)
            .Map(d => d.ModifiedAt, s => s.CreatedAt)
            .Map(d => d.ModifiedBy, s => s.CreatedBy);
    }
    private static void RegisterCommonMappings(TypeAdapterConfig config)
    {
        config.NewConfig<UserDetails, UserDetailsModel>()
            .MapToConstructor(true)
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.FullName, s => s.FullName)
            .ShallowCopyForSameType(false);
    }
    private IEnumerable<double> GetListDoubleFromString(string stringValue) => stringValue.Split(';').Select(Double.Parse).ToList();
    private IEnumerable<Core.Domain.Values.BodyElements> GetListBodyElementsFromString(string stringValue) => stringValue.Split(';').Select(x =>(Core.Domain.Values.BodyElements) Enum.Parse(typeof(Core.Domain.Values.BodyElements),x)).ToList();
}
