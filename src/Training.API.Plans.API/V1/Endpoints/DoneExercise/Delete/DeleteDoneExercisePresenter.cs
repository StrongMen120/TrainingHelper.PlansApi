using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.Delete;

public class DeleteDoneExercisePresenter : ActionResultPresenterBase<DeleteDoneExerciseUseCase>, DeleteDoneExerciseUseCase.IOutput
{
    public DeleteDoneExercisePresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundExercise(DeleteDoneExerciseUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"DoneExercise not found!.", input));

    public void Success(DoneExercisesModel doneExercise) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<DoneExercisesDto>(doneExercise)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}