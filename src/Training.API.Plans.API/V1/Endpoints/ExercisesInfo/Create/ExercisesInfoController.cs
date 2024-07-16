using Microsoft.AspNetCore.Mvc;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API.Controllers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Training.Common.Hexagon.Core;
using Training.API.Plans.API.V1.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.Create;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/exercises-info")]
public sealed class ExercisesInfoController : SimpleUseCaseController<CreateExerciseInfoUseCase, CreateExerciseInfoPresenter>
{
    public ExercisesInfoController(ILogger logger, CreateExerciseInfoUseCase useCase, CreateExerciseInfoPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPost(Name = nameof(CreateExerciseInfo))]
    [SwaggerOperation(summary: "Create exercise info")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(ExercisesInfoDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> CreateExerciseInfo([FromBody] CreateExerciseInfoCommand command)
    {
        CreateExerciseInfoUseCase.Input input = new(command.Name, command.Description, command.AuthorId, command.BodyElements);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}