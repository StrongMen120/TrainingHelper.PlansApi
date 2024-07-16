using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.Attachment.Delete;

public class DeleteAttachmentExerciseInfoPresenter : ActionResultPresenterBase<DeleteAttachmentExerciseInfoUseCase>, DeleteAttachmentExerciseInfoUseCase.IOutput
{
    public DeleteAttachmentExerciseInfoPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundExercise(DeleteAttachmentExerciseInfoUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"ExerciseInfo ${input.ExerciseId} not found!.", input));

    public void Success(ExercisesInfoDetailsModel exercises) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<ExercisesInfoDetailsDto>(exercises)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}