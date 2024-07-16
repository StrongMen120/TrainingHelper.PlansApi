using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.PlannedTraining.Delete;

public class DeletePlannedTrainingPresenter : ActionResultPresenterBase<DeletePlannedTrainingUseCase>, DeletePlannedTrainingUseCase.IOutput
{
    public DeletePlannedTrainingPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundPlannedTraining(DeletePlannedTrainingUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"Planned Training with id:{input.Identifier} not found!.", input));

    public void Success(PlannedTrainingsModel plannedTrainingModel) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<PlannedTrainingsDto>(plannedTrainingModel)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}