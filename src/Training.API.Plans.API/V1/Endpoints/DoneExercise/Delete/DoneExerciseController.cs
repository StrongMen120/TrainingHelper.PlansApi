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

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.Delete;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/done-exercises")]
public sealed class DoneExerciseController : SimpleUseCaseController<DeleteDoneExerciseUseCase, DeleteDoneExercisePresenter>
{
    public DoneExerciseController(ILogger logger, DeleteDoneExerciseUseCase useCase, DeleteDoneExercisePresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpDelete("{doneExerciseId}", Name = nameof(DeleteDoneExercise))]
    [SwaggerOperation(summary: "Delete done exercises")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(DoneExercisesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> DeleteDoneExercise(Guid doneExerciseId)
    {
        await this.UseCase.Execute(new DeleteDoneExerciseUseCase.Input(doneExerciseId), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}