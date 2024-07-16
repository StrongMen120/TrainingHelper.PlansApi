
using Microsoft.AspNetCore.Mvc;
using Training.Common.Hexagon.API.Controllers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Training.Common.Hexagon.Core;
using Training.API.Plans.API.V1.DTOs;
using Microsoft.AspNetCore.Authorization;
using Training.API.Plans.Core.UseCases;

namespace Training.API.Plans.API.V1.Endpoints.Attachment.Delete;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/attachment")]
public sealed class AttachmentController : SimpleUseCaseController<DeleteAttachmentExerciseInfoUseCase, DeleteAttachmentExerciseInfoPresenter>
{
    public AttachmentController(ILogger logger, DeleteAttachmentExerciseInfoUseCase useCase, DeleteAttachmentExerciseInfoPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpDelete("{key}/exercises-info/{identifier}", Name = nameof(DeleteExercisesInfoAttachment))]
    [SwaggerOperation(summary: "Deleted exercises-info attachment")]
    [Produces("image/jpeg", "application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(ExercisesInfoDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> DeleteExercisesInfoAttachment(string key, long identifier)
    {
        DeleteAttachmentExerciseInfoUseCase.Input input = new(identifier, key);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}