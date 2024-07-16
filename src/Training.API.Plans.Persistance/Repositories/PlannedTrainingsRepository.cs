
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
internal class PlannedTrainingsRepository : IPlannedTrainingsRepository
{
    private IPersistanceMapper mapper;
    private TrainingDbContext TrainingDbContext;
    private readonly IAuthenticationDetailsProvider authenticationDetailsProvider;

    public PlannedTrainingsRepository(IPersistanceMapper mapper, TrainingDbContext trainingDatabase, IAuthenticationDetailsProvider authenticationDetailsProvider)
    {
        this.mapper = mapper;
        this.authenticationDetailsProvider = authenticationDetailsProvider;
        this.TrainingDbContext = trainingDatabase;
    }

    public async Task<PlannedTrainingsModel?> GetAsync(IPlannedTrainingsRepository.GetById getModel) => await this.FindAsync(getModel);

    public async Task<PlannedTrainingsModel?> FindAsync(IPlannedTrainingsRepository.GetById getModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.PlannedTrainings
                .AsQueryable()
                .Include(e => e.Plans).FirstOrDefaultAsync(e => e.Identifier == getModel.Identifier);
            
            return this.mapper.Map<PlannedTrainingsModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while getting Planned Trainings, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<PlannedTrainingsModel>> SearchAsync(IPlannedTrainingsRepository.SearchAllUserPlannedTrainings searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.PlannedTrainings
                .Where(e => e.UserId == searchModel.UserId).ToListAsync();

            return this.mapper.Map<IEnumerable<PlannedTrainingsModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Planned Trainings, see inter exception for details.", ex);
        }
    }

    public async Task<PlannedTrainingsModel> CreateAsync(IPlannedTrainingsRepository.CreatePlannedTrainings creationModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();

            // var validPlans = await this.TrainingDbContext.PlannedTrainings
            //     .Where(p => p.PlansType == this.mapper.Map<PlansType>(creationModel.PlansType))
            //     .Where(p => creationModel.DateEnd > p.DateStart)
            //     .Where(p => creationModel.DateStart < p.DateEnd).ToListAsync();

            // if(validPlans.Any()) throw new PersistanceException($"Planned Training overlaps other trainings!");

            var newExerciseInfo = new PlannedTrainingsEntity
            {
                Identifier = default,
                DateEnd = creationModel.DateEnd,
                DateStart = creationModel.DateStart,
                GroupId = creationModel.GroupId,
                PlansId = creationModel.PlansId,
                PlansType = this.mapper.Map<PlansType>(creationModel.PlansType),
                TrainerId = creationModel.TrainerId,
                UserId = creationModel.UserId,
                CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
                CreatedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id },
            };

            var entry = await this.TrainingDbContext.AddAsync(newExerciseInfo);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<PlannedTrainingsModel>(entry.Entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while create Planned Training, see inter exception for details.", ex);
        }
    }

    public async Task<PlannedTrainingsModel> UpdateAsync(IPlannedTrainingsRepository.UpdatePlannedTrainings updateModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();
            
            var entry = await this.TrainingDbContext.PlannedTrainings
                .FirstOrDefaultAsync(a => a.Identifier == updateModel.Identifier);

            if (entry == default) throw new NotFoundException($"Planned Training with id: '{updateModel.Identifier}' not found!");

            var validPlans = await this.TrainingDbContext.PlannedTrainings
                .Where(p => p.PlansType == this.mapper.Map<PlansType>(updateModel.PlansType) && p.Identifier != updateModel.Identifier)
                .Where(p => updateModel.DateEnd > p.DateStart)
                .Where(p => updateModel.DateStart < p.DateEnd).ToListAsync();

            if(validPlans.Any()) throw new PersistanceException($"Planned Training overlaps other trainings!");

            entry.DateEnd = updateModel.DateEnd;
            entry.DateStart = updateModel.DateStart;
            entry.GroupId = updateModel.GroupId;
            entry.PlansId = updateModel.PlansId;
            entry.PlansType = this.mapper.Map<PlansType>(updateModel.PlansType);
            entry.TrainerId = updateModel.TrainerId;
            entry.UserId = updateModel.UserId;
            entry.ModifiedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id };
            entry.ModifiedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime;
            this.TrainingDbContext.PlannedTrainings.Update(entry);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<PlannedTrainingsModel>(entry);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while create Planned Training, see inter exception for details.", ex);
        }
    }

    public async Task<PlannedTrainingsModel> DeleteByIdAsync(IPlannedTrainingsRepository.DeletePlannedTrainings id)
    {
        try
        {
            var entity = await this.TrainingDbContext.PlannedTrainings
                .FirstOrDefaultAsync(e => e.Identifier == id.Identifier);

            if (entity == default) throw new NotFoundException($"Planned Trainings with id: '{id.Identifier}' not found!");

            this.TrainingDbContext.PlannedTrainings.Remove(entity);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<PlannedTrainingsModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while deleted Planned Trainings, see inter exception for details.", ex);
        }
    }

    public async Task<PlannedTrainingsModel> DeleteAsync(PlannedTrainingsModel model) => (await this.DeleteByIdAsync(new IPlannedTrainingsRepository.DeletePlannedTrainings(model.Identifier))) ?? throw new NotFoundException($"Planned Trainings with id: '{model.Identifier}' not found!");

    public async Task<IEnumerable<PlannedTrainingsModel>> SearchAsync(IPlannedTrainingsRepository.SearchAllGroupPlannedTrainings searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.PlannedTrainings
                .Where(e => e.GroupId == searchModel.GroupId).ToListAsync();

            return this.mapper.Map<IEnumerable<PlannedTrainingsModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Planned Trainings, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<PlannedTrainingsModel>> SearchAsync(IPlannedTrainingsRepository.SearchAllTrainerPlannedTrainings searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.PlannedTrainings
                .Where(e => e.TrainerId == searchModel.TrainerId).ToListAsync();

            return this.mapper.Map<IEnumerable<PlannedTrainingsModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Planned Trainings, see inter exception for details.", ex);
        }
    }
}
