
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

namespace Training.API.Plans.API.V1.Endpoints.Attachment.Update;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/attachment")]
public sealed class AttachmentController : SimpleUseCaseController<UpdateAttachmentExerciseInfoUseCase, UpdateAttachmentExerciseInfoPresenter>
{
    public AttachmentController(ILogger logger, UpdateAttachmentExerciseInfoUseCase useCase, UpdateAttachmentExerciseInfoPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPut("exercises-info/{identifier}", Name = nameof(UpdateExercisesInfoAttachment))]
    [SwaggerOperation(summary: "Updated exercises-info attachment")]
    [Consumes("multipart/form-data")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(ExercisesInfoDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> UpdateExercisesInfoAttachment(long identifier, IFormFile photoFile)
    {
        UpdateAttachmentExerciseInfoUseCase.Input input = new(identifier, photoFile);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}