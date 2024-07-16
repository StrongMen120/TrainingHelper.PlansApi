
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Persistance.TrainingsDb;
using Training.API.Plans.Persistance.TrainingsDb.Entities;
using Training.Common.Hexagon.Core.Exceptions;
using Training.Common.Hexagon.Core.Repositories;
using Training.Common.Hexagon.Persistance;
using Training.Common.Hexagon.Persistance.Exceptions;

namespace Training.API.Plans.Persistance.Repositories;
internal class DoneExercisesRepository : IDoneExercisesRepository
{
    private IPersistanceMapper mapper;
    private TrainingDbContext TrainingDbContext;
    private readonly IAuthenticationDetailsProvider authenticationDetailsProvider;
    private readonly IExercisesRecordsRepository exercisesRecordsRepository;

    public DoneExercisesRepository(IPersistanceMapper mapper, TrainingDbContext trainingDatabase, IAuthenticationDetailsProvider authenticationDetailsProvider, IExercisesRecordsRepository exercisesRecordsRepository)
    {
        this.mapper = mapper;
        this.authenticationDetailsProvider = authenticationDetailsProvider;
        this.TrainingDbContext = trainingDatabase;
        this.exercisesRecordsRepository = exercisesRecordsRepository;
    }

    public async Task<DoneExercisesModel?> GetAsync(IDoneExercisesRepository.GetById getModel) => await this.FindAsync(getModel);

    public async Task<DoneExercisesModel?> FindAsync(IDoneExercisesRepository.GetById getModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.DoneExercises
                .AsQueryable()
                .Include(e => e.ExerciseInfo).FirstOrDefaultAsync(e => e.Identifier == getModel.Identifier);
            
            return this.mapper.Map<DoneExercisesModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while getting Done Exercises, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<DoneExercisesModel>> SearchAsync(IDoneExercisesRepository.SearchDoneExerciseHistory searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.DoneExercises
                .AsQueryable()
                .Include(e => e.ExerciseInfo)
                .Where(e => e.UserId == searchModel.UserId && e.ExerciseInfoId == searchModel.ExerciseId).ToListAsync();

            return this.mapper.Map<IEnumerable<DoneExercisesModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Done Exercises, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<DoneExercisesModel>> SearchAsync(IDoneExercisesRepository.SearchDoneExercisesAtDay searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.DoneExercises
                .AsQueryable()
                .Include(e => e.ExerciseInfo)
                .Where(e => e.UserId == searchModel.UserId && e.Date == searchModel.Date).ToListAsync();

            return this.mapper.Map<IEnumerable<DoneExercisesModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Done Exercises, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<DoneExercisesModel>> SearchAsync(IDoneExercisesRepository.SearchAllDoneExercises searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.DoneExercises
                .AsQueryable()
                .Include(e => e.ExerciseInfo)
                .Where(e => e.UserId == searchModel.UserId).ToListAsync();
            return this.mapper.Map<IEnumerable<DoneExercisesModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Done Exercises, see inter exception for details.", ex);
        }
    }


    public async Task<DoneExercisesModel> UpdateAsync(IDoneExercisesRepository.UpdateDoneExercises updateModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();
            
            var entry = await this.TrainingDbContext.DoneExercises
                .FirstOrDefaultAsync(a => a.Identifier == updateModel.Identifier);

            if (entry == default) throw new NotFoundException($"Done Exercises with id: '{updateModel.Identifier}' not found!");
            
            entry.Series = updateModel.createdDoneExercises.Series;
            entry.Reps = string.Join(";", updateModel.createdDoneExercises.Reps);
            entry.Weight = string.Join(";", updateModel.createdDoneExercises.Weight);
            entry.Rate = updateModel.createdDoneExercises.Rate;
            entry.RPE = updateModel.createdDoneExercises.Rpe;
            entry.BrakeSeconds = updateModel.createdDoneExercises.BrakeSeconds;
            entry.Date = updateModel.Date;
            entry.ModifiedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id };
            entry.ModifiedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime;
            this.TrainingDbContext.DoneExercises.Update(entry);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<DoneExercisesModel>(entry);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while updated Done Exercise, see inter exception for details.", ex);
        }
    }

    public async Task<DoneExercisesModel> CreateAsync(IDoneExercisesRepository.CreateDoneExercises creationModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();

            var newDoneExerciseInfo = new DoneExercisesEntity
            {
                Identifier = default,
                BrakeSeconds = creationModel.createdDoneExercises.BrakeSeconds,
                Date = creationModel.Date,
                ExerciseInfoId = creationModel.createdDoneExercises.ExerciseInfoId,
                Rate = creationModel.createdDoneExercises.Rate,
                Reps = string.Join(";", creationModel.createdDoneExercises.Reps),
                RPE = creationModel.createdDoneExercises.Rpe,
                Series = creationModel.createdDoneExercises.Series,
                UserId = creationModel.UserId,
                Weight = string.Join(";", creationModel.createdDoneExercises.Weight),
                CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
                CreatedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id },
            };
            
            var entry = await this.TrainingDbContext.AddAsync(newDoneExerciseInfo);
            await this.TrainingDbContext.SaveChangesAsync();
            await this.exercisesRecordsRepository.CreateAsync(new IExercisesRecordsRepository.CreateNewExerciseRecordsFromDoneExercise(creationModel.createdDoneExercises.ExerciseInfoId, creationModel.UserId, creationModel.createdDoneExercises.Weight, creationModel.createdDoneExercises.Reps));
            return this.mapper.Map<DoneExercisesModel>(entry.Entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while create Exercise Info, see inter exception for details.", ex);
        }
    }
    
    public async Task<DoneExercisesModel> DeleteByIdAsync(IDoneExercisesRepository.DeleteDoneExercise id)
    {
        try
        {
            var entity = await this.TrainingDbContext.DoneExercises
                .FirstOrDefaultAsync(e => e.Identifier == id.Identifier);

            if (entity == default) throw new NotFoundException($"Done Exercises with id: '{id.Identifier}' not found!");

            this.TrainingDbContext.DoneExercises.Remove(entity);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<DoneExercisesModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while deleted Done Exercise, see inter exception for details.", ex);
        }
    }

    public async Task<DoneExercisesModel> DeleteAsync(DoneExercisesModel model) => (await this.DeleteByIdAsync(new IDoneExercisesRepository.DeleteDoneExercise(model.Identifier))) ?? throw new NotFoundException($"Done Exercises with id: '{model.Identifier}' not found!");
}
