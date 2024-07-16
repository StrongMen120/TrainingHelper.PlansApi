
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

namespace Training.API.Plans.API.V1.Endpoints.Attachment.Get;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/exercises-info")]
public sealed class AttachmentController : SimpleUseCaseController<GetAttachmentExerciseInfoUseCase, GetAttachmentExerciseInfoPresenter>
{
    public AttachmentController(ILogger logger, GetAttachmentExerciseInfoUseCase useCase, GetAttachmentExerciseInfoPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("{identifier}/attachment", Name = nameof(GetExercisesInfoAttachment))]
    [SwaggerOperation(summary: "Get exercises-info attachment")]
    [Produces("image/jpeg", "application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(ExercisesInfoDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetExercisesInfoAttachment(long identifier)
    {
        GetAttachmentExerciseInfoUseCase.Input input = new(identifier);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}