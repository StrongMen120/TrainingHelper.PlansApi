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

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.Delete;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/exercises-info")]
public sealed class ExercisesInfoController : SimpleUseCaseController<DeleteExerciseInfoUseCase, DeleteExerciseInfoPresenter>
{
    public ExercisesInfoController(ILogger logger, DeleteExerciseInfoUseCase useCase, DeleteExerciseInfoPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpDelete("{identifier}", Name = nameof(DeleteExerciseInfo))]
    [SwaggerOperation(summary: "Delete exercise info")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(ExercisesInfoDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> DeleteExerciseInfo(long identifier)
    {
        DeleteExerciseInfoUseCase.Input input = new(identifier);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}