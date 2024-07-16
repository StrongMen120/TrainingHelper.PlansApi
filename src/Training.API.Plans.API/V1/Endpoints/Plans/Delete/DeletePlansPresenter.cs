using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.Plans.Delete;

public class DeletePlansPresenter : ActionResultPresenterBase<DeletePlansUseCase>, DeletePlansUseCase.IOutput
{
    public DeletePlansPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }


    public void NotFoundPlan(DeletePlansUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"Plans ${input} not found!.", input));

    public void Success(PlansModel plansModel) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<PlansDto>(plansModel)));
    
    public void UnknownError(Exception exception) => this.SetException(exception);
}