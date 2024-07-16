using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.PlannedTraining.GetAll;

public class GetAllUserPlannedTrainingPresenter : ActionResultPresenterBase<GetAllUserPlannedTrainingUseCase>, GetAllUserPlannedTrainingUseCase.IOutput
{
    public GetAllUserPlannedTrainingPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void Success(IEnumerable<PlannedTrainingsModel> plannedTrainingsModel) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<IEnumerable<PlannedTrainingsDto>>(plannedTrainingsModel)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}