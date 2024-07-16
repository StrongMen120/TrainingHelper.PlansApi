using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.Delete;

public class DeleteExerciseInfoPresenter : ActionResultPresenterBase<DeleteExerciseInfoUseCase>, DeleteExerciseInfoUseCase.IOutput
{
    public DeleteExerciseInfoPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundExercise(DeleteExerciseInfoUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"ExerciseInfo ${input.ExerciseId} not found!.", input));

    public void Success(ExercisesInfoModel exerciseInfo) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<ExercisesInfoDto>(exerciseInfo)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}