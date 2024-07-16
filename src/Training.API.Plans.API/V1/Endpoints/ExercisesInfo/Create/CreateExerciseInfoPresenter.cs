using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.Create;

public class CreateExerciseInfoPresenter : ActionResultPresenterBase<CreateExerciseInfoUseCase>, CreateExerciseInfoUseCase.IOutput
{
    public CreateExerciseInfoPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void DuplicateName(ExercisesInfoModel exerciseInfo) => this.SetResult(ActionResultFactory.Conflicts409($"ExerciseInfo witch name: ${exerciseInfo.Name} exists!.", this.Mapper.Map<ExercisesInfoDto>(exerciseInfo)));
    public void Success(ExercisesInfoModel exerciseInfo) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<ExercisesInfoDto>(exerciseInfo)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}