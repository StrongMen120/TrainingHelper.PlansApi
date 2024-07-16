using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.PlannedTraining.Update;

public class UpdatePlannedTrainingPresenter : ActionResultPresenterBase<UpdatePlannedTrainingUseCase>, UpdatePlannedTrainingUseCase.IOutput
{
    public UpdatePlannedTrainingPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundPlan(UpdatePlannedTrainingUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"Plans with id:{input.PlansId} not found!.", input));

    public void NotFoundPlannedTraining(UpdatePlannedTrainingUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"Planned Training with id:{input.Identifier} not found!.", input));

    public void Success(PlannedTrainingsModel plannedTrainingModel) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<PlannedTrainingsDto>(plannedTrainingModel)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}