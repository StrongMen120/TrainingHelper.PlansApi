using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.Plans.Create;

public class CreatePlansPresenter : ActionResultPresenterBase<CreatePlansUseCase>, CreatePlansUseCase.IOutput
{
    public CreatePlansPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void DuplicateName(PlansModel plansModel) => this.SetResult(ActionResultFactory.Conflicts409($"Plans witch name:{plansModel.Name} exists!.", this.Mapper.Map<PlansDto>(plansModel)));

    public void NotFoundExercise(CreatePlansUseCase.Input input)=> this.SetResult(ActionResultFactory.NotFound404($"Exercise not Found!."));

    public void NotFoundUser(CreatePlansUseCase.Input input)=> this.SetResult(ActionResultFactory.NotFound404($"User witch id:{input.AuthorId} not found!."));

    public void Success(PlansModel plansModel)=> this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<PlansDto>(plansModel)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}