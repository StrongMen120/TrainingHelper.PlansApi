using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.Update;

public class UpdateDoneExercisePresenter : ActionResultPresenterBase<UpdateDoneExerciseUseCase>, UpdateDoneExerciseUseCase.IOutput
{
    public UpdateDoneExercisePresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundExercise(UpdateDoneExerciseUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"Exercise witch id:${input.ExerciseInfoId} not found!.", input));

    public void Success(DoneExercisesModel doneExercises) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<DoneExercisesDto>(doneExercises)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}