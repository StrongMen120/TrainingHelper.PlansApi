using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.Get;

public class GetExerciseInfoPresenter : ActionResultPresenterBase<GetExerciseInfoUseCase>, GetExerciseInfoUseCase.IOutput
{
    public GetExerciseInfoPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
        { }

    public void NotFoundExercise(GetExerciseInfoUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"ExerciseInfo ${input.ExerciseId} not found!.", input));

    public void Success(ExercisesInfoModel exerciseInfo) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<ExercisesInfoDto>(exerciseInfo)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}