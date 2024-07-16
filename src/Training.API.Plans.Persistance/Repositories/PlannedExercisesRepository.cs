
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
internal class PlannedExercisesRepository : IPlannedExercisesRepository
{
    private IPersistanceMapper mapper;
    private TrainingDbContext TrainingDbContext;
    private readonly IAuthenticationDetailsProvider authenticationDetailsProvider;

    public PlannedExercisesRepository(IPersistanceMapper mapper, TrainingDbContext trainingDatabase, IAuthenticationDetailsProvider authenticationDetailsProvider)
    {
        this.mapper = mapper;
        this.authenticationDetailsProvider = authenticationDetailsProvider;
        this.TrainingDbContext = trainingDatabase;
    }

    public async Task<PlannedExercisesModel?> GetAsync(IPlannedExercisesRepository.GetById getModel) => await this.FindAsync(getModel);

    public async Task<PlannedExercisesModel?> FindAsync(IPlannedExercisesRepository.GetById getModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.PlannedExercises
                .AsQueryable()
                .Include(e => e.ExerciseInfo).FirstOrDefaultAsync(e => e.Identifier == getModel.Identifier);
            
            return this.mapper.Map<PlannedExercisesModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while getting Planned Exercises, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<PlannedExercisesModel>> SearchAsync(IPlannedExercisesRepository.SearchAllPlannedExercises searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.PlannedExercises
                .AsQueryable()
                .Include(e => e.ExerciseInfo)
                .Where(e => e.PlansId == searchModel.PlanId).ToListAsync();

            return this.mapper.Map<IEnumerable<PlannedExercisesModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Planned Exercises, see inter exception for details.", ex);
        }
    }

    public async Task<PlannedExercisesModel> DeleteByIdAsync(IPlannedExercisesRepository.DeletePlannedExercises id)
    {
                try
        {
            var entity = await this.TrainingDbContext.PlannedExercises
                .FirstOrDefaultAsync(e => e.Identifier == id.Identifier);

            if (entity == default) throw new NotFoundException($"Planned Exercises with id: '{id.Identifier}' not found!");

            this.TrainingDbContext.PlannedExercises.Remove(entity);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<PlannedExercisesModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while deleted Planned Exercises, see inter exception for details.", ex);
        }
    }

    public async Task<PlannedExercisesModel> DeleteAsync(PlannedExercisesModel model) => (await this.DeleteByIdAsync(new IPlannedExercisesRepository.DeletePlannedExercises(model.Identifier))) ?? throw new NotFoundException($"Planned Exercises with id: '{model.Identifier}' not found!");
}
