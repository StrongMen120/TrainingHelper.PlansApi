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

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.Update;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/exercises-info")]
public sealed class ExercisesInfoController : SimpleUseCaseController<UpdateExerciseInfoUseCase, UpdateExerciseInfoPresenter>
{
    public ExercisesInfoController(ILogger logger, UpdateExerciseInfoUseCase useCase, UpdateExerciseInfoPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPut(Name = nameof(UpdateExerciseInfo))]
    [SwaggerOperation(summary: "Update exercise info")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(ExercisesInfoDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> UpdateExerciseInfo([FromBody] UpdateExerciseInfoCommand command)
    {
        UpdateExerciseInfoUseCase.Input input = new(command.UpdatedId, command.Name, command.Description, command.BodyElements);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}