using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.Attachment.Get;

public class GetAttachmentExerciseInfoPresenter : ActionResultPresenterBase<GetAttachmentExerciseInfoUseCase>, GetAttachmentExerciseInfoUseCase.IOutput
{
    public GetAttachmentExerciseInfoPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundExercise(GetAttachmentExerciseInfoUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"ExerciseInfo ${input.ExerciseId} not found!.", input));

    public void Success(ExercisesInfoDetailsModel exercisesInfoDetails) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<ExercisesInfoDetailsDto>(exercisesInfoDetails)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}