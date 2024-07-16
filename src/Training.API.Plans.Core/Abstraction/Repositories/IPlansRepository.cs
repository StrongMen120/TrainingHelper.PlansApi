
using NodaTime;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.Domain.Values;
using Training.Common.Hexagon.Core.Repositories;

namespace Training.API.Plans.Core.Abstraction.Repositories;

public interface IPlansRepository  :
    IGetRepository<IPlansRepository.GetById, PlansModel?>,
    IGetRepository<IPlansRepository.GetByName, PlansModel?>,
    ISearchRepository<IPlansRepository.SearchAllPlans, PlansModel>,
    ICreateRepository<IPlansRepository.CreatePlans, PlansModel>,
    IUpdateRepository<IPlansRepository.UpdatePlans, PlansModel>,
    IDeleteRepository<IPlansRepository.DeletePlans, PlansModel>
{   
    #region Gets

    public record class GetById(long Identifier);
    public record class GetByName(string Name);

    #endregion Gets

    #region Searches

    public record class SearchAllPlans(long UserId);

    #endregion Searches

    #region Crete & Updates

    public record class CreatePlans(string Name, string Description, PlansImage PlansImage, long AuthorId, IEnumerable<OneCreatedPlannedExerciseModel> PlannedExercise);
    public record class UpdatePlans(long Identifier, string Name, string Description, PlansImage PlansImage, IEnumerable<OneUpdatedPlannedExerciseModel> PlannedExercise);

    #endregion Crete & Updates

    #region Delete

    public record class DeletePlans(long Identifier);

    #endregion Delete
}