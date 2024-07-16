using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.Plans.GetAll;

public class GetAllUserPlansPresenter : ActionResultPresenterBase<GetAllUserPlansUseCase>, GetAllUserPlansUseCase.IOutput
{
    public GetAllUserPlansPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundUser(GetAllUserPlansUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"User witch id:${input.UserId} not found!.", input));

    public void Success(IEnumerable<PlansModel> plans) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<IEnumerable<PlansDto>>(plans)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}