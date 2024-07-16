
using NodaTime;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.Domain.Values;
using Training.Common.Hexagon.Core.Repositories;

namespace Training.API.Plans.Core.Abstraction.Repositories;

public interface IPlannedTrainingsRepository  :
    IGetRepository<IPlannedTrainingsRepository.GetById, PlannedTrainingsModel?>,
    ISearchRepository<IPlannedTrainingsRepository.SearchAllUserPlannedTrainings, PlannedTrainingsModel>,
    ISearchRepository<IPlannedTrainingsRepository.SearchAllGroupPlannedTrainings, PlannedTrainingsModel>,
    ISearchRepository<IPlannedTrainingsRepository.SearchAllTrainerPlannedTrainings, PlannedTrainingsModel>,
    ICreateRepository<IPlannedTrainingsRepository.CreatePlannedTrainings, PlannedTrainingsModel>,
    IUpdateRepository<IPlannedTrainingsRepository.UpdatePlannedTrainings, PlannedTrainingsModel>,
    IDeleteRepository<IPlannedTrainingsRepository.DeletePlannedTrainings, PlannedTrainingsModel>
{   
    #region Gets

    public record class GetById(Guid Identifier);

    #endregion Gets

    #region Searches

    public record class SearchAllUserPlannedTrainings(long UserId);
    public record class SearchAllGroupPlannedTrainings(long GroupId);
    public record class SearchAllTrainerPlannedTrainings(long TrainerId);

    #endregion Searches

    #region Crete & Updates

    public record class CreatePlannedTrainings(
        long PlansId,
        PlansType PlansType,
        LocalDateTime DateStart,
        LocalDateTime DateEnd,
        long? UserId,
        long? TrainerId,
        long? GroupId);
    public record class UpdatePlannedTrainings(
        Guid Identifier,
        long PlansId,
        PlansType PlansType,
        LocalDateTime DateStart,
        LocalDateTime DateEnd,
        long? UserId,
        long? TrainerId,
        long? GroupId);

    #endregion Crete & Updates

    #region Delete

    public record class DeletePlannedTrainings(Guid Identifier);

    #endregion Delete
}