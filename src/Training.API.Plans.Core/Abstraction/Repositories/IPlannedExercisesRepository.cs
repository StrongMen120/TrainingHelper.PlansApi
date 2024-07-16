using NodaTime;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core.Repositories;

namespace Training.API.Plans.Core.Abstraction.Repositories;

public interface IPlannedExercisesRepository  :
    IGetRepository<IPlannedExercisesRepository.GetById, PlannedExercisesModel?>,
    ISearchRepository<IPlannedExercisesRepository.SearchAllPlannedExercises, PlannedExercisesModel>,
    IDeleteRepository<IPlannedExercisesRepository.DeletePlannedExercises, PlannedExercisesModel>
{   
    #region Gets

    public record class GetById(Guid Identifier);

    #endregion Gets

    #region Searches

    public record class SearchAllPlannedExercises(long PlanId);

    #endregion Searches

    #region Crete & Updates

    #endregion Crete & Updates

    #region Delete

    public record class DeletePlannedExercises(Guid Identifier);

    #endregion Delete
}