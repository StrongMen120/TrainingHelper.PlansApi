using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Persistance.TrainingsDb;
using Training.Common.Hexagon.Persistance;
using Training.Common.Hexagon.Persistance.Exceptions;

namespace Training.API.Plans.Persistance.Repositories;
internal class ExercisesRecordsRepository : IExercisesRecordsRepository
{
    private IPersistanceMapper mapper;
    private TrainingDbContext TrainingDbContext;
    private readonly IAuthenticationDetailsProvider authenticationDetailsProvider;

    public ExercisesRecordsRepository(IPersistanceMapper mapper, TrainingDbContext trainingDatabase, IAuthenticationDetailsProvider authenticationDetailsProvider)
    {
        this.mapper = mapper;
        this.authenticationDetailsProvider = authenticationDetailsProvider;
        this.TrainingDbContext = trainingDatabase;
    }

    public async Task<IEnumerable<ExercisesRecordsModel?>> SearchAsync(IExercisesRecordsRepository.SearchAllExercisesRecordsToUser searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.ExerciseRecordsRegistry
                .Include(x => x.ExerciseRecords)
                .FirstOrDefaultAsync(e => e.UserId == searchModel.UserId && e.ExerciseId == searchModel.ExerciseId);

            return this.mapper.Map<IEnumerable<ExercisesRecordsModel>>(entity.ExerciseRecords);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Exercise Records, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<ExercisesRecordsModel?>> SearchAsync(IExercisesRecordsRepository.SearchAllExercisesBestRecordsToUser searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.ExerciseRecordsRegistry
                .Include(x => x.ExerciseRecords)
                .Where(e => e.UserId == searchModel.UserId).ToListAsync();
            var result = entity.Select(e => e.ExerciseRecords.FirstOrDefault(e => e.Revision == e.RegistryEntry.LatestRevision));
            return this.mapper.Map<IEnumerable<ExercisesRecordsModel>>(result);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Exercise Records, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<StatisticsModel>> SearchAsync(IExercisesRecordsRepository.SearchExercisesStatistics searchModel)
    {
        try
        {
            var recordsInfo = await this.TrainingDbContext.ExerciseRecordsRegistry
                .Include(a => a.ExerciseRecords)
                .FirstOrDefaultAsync(p => p.ExerciseId == searchModel.ExerciseId && p.UserId == searchModel.UserId);

            if (recordsInfo == null)
                return null;

            var record = recordsInfo.ExerciseRecords.FirstOrDefault(e => e.Revision == e.RegistryEntry.LatestRevision);
            var doneWorkouts = await this.TrainingDbContext.DoneExercises
                .Where(p => p.ExerciseInfoId == searchModel.ExerciseId && p.UserId == searchModel.UserId)
                .Where(p => p.Date.Year == searchModel.year && p.Date.Month == searchModel.month).ToListAsync();

            return this.CalculateStatistics(this.mapper.Map<IEnumerable<DoneExercisesModel>>(doneWorkouts), record);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Exercise statistics, see inter exception for details.", ex);
        }
    }

    public async Task<ExercisesRecordsModel?> CreateAsync(IExercisesRecordsRepository.CreateNewExerciseRecords creationModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();
            var timeNow = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime;

            var exerciseRecords = await this.TrainingDbContext.ExerciseRecordsRegistry
                                .Include(e => e.ExerciseRecords)
                                .FirstOrDefaultAsync(p => p.ExerciseId == creationModel.ExerciseId && p.UserId == creationModel.UserId);

            var record = this.CalculateRecords(creationModel.Reps, creationModel.Weight);

            if (exerciseRecords == default)
            {
                var entry = await this.TrainingDbContext.ExerciseRecordsRegistry.AddAsync(new()
                {
                    Identifier = default,
                    ExerciseId = creationModel.ExerciseId,
                    UserId = creationModel.UserId,
                    LatestRevision = 1,
                    CreatedAt = timeNow,
                    CreatedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id },
                });
                await this.TrainingDbContext.SaveChangesAsync();
                var result = await this.TrainingDbContext.ExerciseRecords.AddAsync(new()
                {
                    Identifier = entry.Entity.Identifier,
                    Revision = entry.Entity.LatestRevision,
                    AdamsResult = record.AdamsResult,
                    BaechleResult = record.BaechleResult,
                    BergerResult = record.BergerResult,
                    BrownResult = record.BrownResult,
                    BrzyckiResult = record.BrzyckiResult,
                    Date = timeNow.Date,
                    EpleyResult = record.EpleyResult,
                    isAutomat = creationModel.isAutomat,
                    LombardiResult = record.LombardiResult,
                    MayhewResult = record.MayhewResult,
                    OneRepetitionMaximum = record.OneRepetitionMaximum,
                    Reps = creationModel.Reps,
                    Weight = creationModel.Weight,
                    CreatedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id },
                    CreatedAt = timeNow,
                });
                await this.TrainingDbContext.SaveChangesAsync();
                return this.mapper.Map<ExercisesRecordsModel>(result.Entity);
            }
            else
            {
                var currentRecord = exerciseRecords.ExerciseRecords.FirstOrDefault(x => x.Revision == x.RegistryEntry.LatestRevision);
                if(currentRecord != default && currentRecord.OneRepetitionMaximum < record.OneRepetitionMaximum)
                {
                    exerciseRecords.LatestRevision++;
                    this.TrainingDbContext.ExerciseRecordsRegistry.Update(exerciseRecords);
                    var result = await this.TrainingDbContext.ExerciseRecords.AddAsync(new()
                    {
                        Identifier = exerciseRecords.Identifier,
                        Revision = exerciseRecords.LatestRevision,
                        AdamsResult = record.AdamsResult,
                        BaechleResult = record.BaechleResult,
                        BergerResult = record.BergerResult,
                        BrownResult = record.BrownResult,
                        BrzyckiResult = record.BrzyckiResult,
                        Date = timeNow.Date,
                        EpleyResult = record.EpleyResult,
                        isAutomat = creationModel.isAutomat,
                        LombardiResult = record.LombardiResult,
                        MayhewResult = record.MayhewResult,
                        OneRepetitionMaximum = record.OneRepetitionMaximum,
                        Reps = creationModel.Reps,
                        Weight = creationModel.Weight,
                        CreatedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id },
                        CreatedAt = timeNow,
                    });
                    await this.TrainingDbContext.SaveChangesAsync();
                    return this.mapper.Map<ExercisesRecordsModel>(result.Entity);
                }
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while creating Exercise Records, see inter exception for details.", ex);
        }
    }

    public async Task<ExercisesRecordsModel?> CreateAsync(IExercisesRecordsRepository.CreateNewExerciseRecordsFromDoneExercise creationModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();
            List<RecordsModel> records = new List<RecordsModel>();
            var Weight = creationModel.Weight.ToArray();
            var Reps = creationModel.Reps.Select(p => Convert.ToInt32(p)).ToArray();
            for (int i = 0; i < Weight.Length; i++)
            {
                records.Add(this.CalculateRecords(Reps[i], Weight[i]));
            }
            var maxRecord = records.MaxBy(x => x.OneRepetitionMaximum);
            return await this.CreateAsync(new IExercisesRecordsRepository.CreateNewExerciseRecords(creationModel.ExerciseId, creationModel.UserId, maxRecord.Weight, maxRecord.Reps));
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while creating Exercise Records from done Exercise, see inter exception for details.", ex);
        }
    }    

    public RecordsModel CalculateRecords(int Reps, double Weight)
    {
        double LombardiResult = Weight * Math.Pow(Reps, 0.1);
        double BrzyckiResult = Weight * (36 / (37 - Reps));
        double EpleyResult = Weight * (1 + (Reps / 30));
        double MayhewResult = Weight * 100 / (52.2 + (41.9 * Math.Exp(-1 * (Reps * 0.055))));
        double AdamsResult = Weight * (1 / (1 - 0.02 * Reps));
        double BaechleResult = Weight * (1 + (0.033 * Reps));
        double BergerResult = Weight * (1 / (1.0261 * Math.Exp(-0.0262 * Reps)));
        double BrownResult = Weight * (0.9849 + (0.0328 * Reps));
        double OneRepetitionMaximum = (LombardiResult + BrzyckiResult + EpleyResult + MayhewResult + AdamsResult + BaechleResult + BergerResult + BrownResult) / 8;
        return new(Reps, Weight, LombardiResult, BrzyckiResult, EpleyResult, MayhewResult, AdamsResult, BaechleResult, BergerResult, BrownResult, OneRepetitionMaximum);
    }
    public IEnumerable<StatisticsModel> CalculateStatistics(IEnumerable<DoneExercisesModel> doneExercises, TrainingsDb.Entities.ExerciseRecordsEntity record)
    {
        var result = new List<StatisticsModel>();
        foreach (var exercise in doneExercises)
        {
            double volume = 0;
            double allReps = 0;
            for (int i = 0; i < exercise.Series; i++)
            {
                allReps += exercise.Reps[i];
                volume = exercise.Reps[i] * exercise.Weight[i];
            }
            double intensity = ((volume/allReps) / record.OneRepetitionMaximum) * 100;
            result.Add(new(exercise.ExerciseInfoId, exercise.UserId, exercise.Date, volume,intensity));
        }
        return result;
    }
}
