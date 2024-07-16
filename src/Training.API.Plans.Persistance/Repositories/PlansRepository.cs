
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
internal class PlansRepository : IPlansRepository
{
    private IPersistanceMapper mapper;
    private TrainingDbContext TrainingDbContext;
    private readonly IAuthenticationDetailsProvider authenticationDetailsProvider;

    public PlansRepository(IPersistanceMapper mapper, TrainingDbContext trainingDatabase, IAuthenticationDetailsProvider authenticationDetailsProvider)
    {
        this.mapper = mapper;
        this.authenticationDetailsProvider = authenticationDetailsProvider;
        this.TrainingDbContext = trainingDatabase;
    }

    public async Task<PlansModel?> GetAsync(IPlansRepository.GetById getModel) => await this.FindAsync(getModel);

    public async Task<PlansModel?> FindAsync(IPlansRepository.GetById getModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.Plans
                .AsQueryable()
                .Include(e => e.PlannedExercises).FirstOrDefaultAsync(e => e.Identifier == getModel.Identifier);
            
            return this.mapper.Map<PlansModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while getting Plans, see inter exception for details.", ex);
        }
    }

    public async Task<PlansModel?> GetAsync(IPlansRepository.GetByName getModel) => await this.FindAsync(getModel);

    public async Task<PlansModel?> FindAsync(IPlansRepository.GetByName getModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.Plans
                .AsQueryable()
                .Include(e => e.PlannedExercises).FirstOrDefaultAsync(e => e.Name == getModel.Name);
            
            return this.mapper.Map<PlansModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while getting Plans, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<PlansModel>> SearchAsync(IPlansRepository.SearchAllPlans searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.Plans
                .Where(e => e.AuthorId == searchModel.UserId).AsQueryable().Include(e => e.PlannedExercises).ToListAsync();

            return this.mapper.Map<IEnumerable<PlansModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Plans, see inter exception for details.", ex);
        }
    }

    public async Task<PlansModel> CreateAsync(IPlansRepository.CreatePlans creationModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();

            var newPlans = new PlansEntity
            {
                Identifier = default,
                AuthorId = creationModel.AuthorId,
                Description = creationModel.Description,
                Name = creationModel.Name,
                Image = this.mapper.Map<PlansImage>(creationModel.PlansImage),
                CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
                CreatedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id },
            };
            var listPlannedExercises = new List<PlannedExercisesEntity>();
            foreach (var item in creationModel.PlannedExercise)
            {
                listPlannedExercises.Add(new(){
                    BrakeSeconds=item.BrakeSeconds,
                    CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
                    CreatedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id },
                    ExerciseInfoId = item.ExerciseInfoId,
                    Identifier = default,
                    Weight = string.Join(";", item.Weight),
                    Series = item.Series,
                    RPE = item.Rpe,
                    Reps = string.Join(";", item.Reps),
                    Rate = item.Rate,
                });
            }
            newPlans.PlannedExercises = listPlannedExercises;
            var entry = await this.TrainingDbContext.AddAsync(newPlans);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<PlansModel>(entry.Entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while create Plans, see inter exception for details.", ex);
        }
    }

    public async Task<PlansModel> UpdateAsync(IPlansRepository.UpdatePlans updateModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();
            var entry = await this.TrainingDbContext.Plans
                .AsQueryable()
                .Include(e=>e.PlannedExercises)
                .FirstOrDefaultAsync(a => a.Identifier == updateModel.Identifier);

            if (entry == default) throw new NotFoundException($"Plans with id: '{updateModel.Identifier}' not found!");
            
            var listPlannedExercises = new List<PlannedExercisesEntity>();
            foreach (var item in updateModel.PlannedExercise)
            {
                listPlannedExercises.Add(new(){
                    BrakeSeconds = item.BrakeSeconds,
                    CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
                    CreatedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id },
                    ExerciseInfoId = item.ExerciseInfoId,
                    Identifier = default,
                    Weight = string.Join(";", item.Weight),
                    Series = item.Series,
                    RPE = item.Rpe,
                    Reps = string.Join(";", item.Reps),
                    Rate = item.Rate,
                });
            }
            this.TrainingDbContext.PlannedExercises.RemoveRange(entry.PlannedExercises);
            entry.Description = updateModel.Description;
            entry.Name = updateModel.Name;
            entry.PlannedExercises = listPlannedExercises;
            entry.Image = this.mapper.Map<PlansImage>(updateModel.PlansImage);
            this.TrainingDbContext.Plans.Update(entry);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<PlansModel>(entry);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while updated Plans, see inter exception for details.", ex);
        }
    }

    public async Task<PlansModel> DeleteByIdAsync(IPlansRepository.DeletePlans id)
    {
        try
        {
            var entity = await this.TrainingDbContext.Plans
                .FirstOrDefaultAsync(e => e.Identifier == id.Identifier);

            if (entity == default) throw new NotFoundException($"Plans with id: '{id.Identifier}' not found!");

            this.TrainingDbContext.Plans.Remove(entity);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<PlansModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while deleted Plans, see inter exception for details.", ex);
        }
    }

    public async Task<PlansModel> DeleteAsync(PlansModel model) => (await this.DeleteByIdAsync(new IPlansRepository.DeletePlans(model.Identifier))) ?? throw new NotFoundException($"Plans with id: '{model.Identifier}' not found!");
}
