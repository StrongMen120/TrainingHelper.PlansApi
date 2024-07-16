using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.GetAll;

public class GetAllDoneExercisePresenter : ActionResultPresenterBase<GetAllDoneExerciseUseCase>, GetAllDoneExerciseUseCase.IOutput
{
    public GetAllDoneExercisePresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundUser(GetAllDoneExerciseUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"User witch id:${input.UserId} not found!.", input));

    public void Success(IEnumerable<DoneExercisesModel> doneExercises) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<IEnumerable<DoneExercisesDto>>(doneExercises)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}