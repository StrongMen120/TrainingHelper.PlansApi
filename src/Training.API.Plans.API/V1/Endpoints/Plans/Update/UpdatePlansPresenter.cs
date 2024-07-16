using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.Plans.Update;

public class UpdatePlansPresenter : ActionResultPresenterBase<UpdatePlansUseCase>, UpdatePlansUseCase.IOutput
{
    public UpdatePlansPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void DuplicateName(PlansModel plansModel) => this.SetResult(ActionResultFactory.Conflicts409($"Plans witch name: ${plansModel.Name} exists!.", this.Mapper.Map<PlansDto>(plansModel)));

    public void NotFoundExercise(UpdatePlansUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"Exercise not found!.", input));

    public void NotFoundPlans(UpdatePlansUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"Plans not found!.", input));

    public void Success(PlansModel plansModel) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<PlansDto>(plansModel)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}