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

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.Update;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/done-exercises")]
public sealed class DoneExerciseController : SimpleUseCaseController<UpdateDoneExerciseUseCase, UpdateDoneExercisePresenter>
{
    public DoneExerciseController(ILogger logger, UpdateDoneExerciseUseCase useCase, UpdateDoneExercisePresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPut("{identifier}", Name = nameof(UpdateDoneExercise))]
    [SwaggerOperation(summary: "Update done exercises")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(DoneExercisesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    public async ValueTask<IActionResult> UpdateDoneExercise(Guid identifier, [FromBody] UpdateDoneExerciseCommand command)
    {
        await this.UseCase.Execute(new UpdateDoneExerciseUseCase.Input(
            identifier,
            command.Date,
            command.ExerciseInfoId,
            command.Series,
            command.Reps,
            command.Weight,
            command.Rate,
            command.RPE,
            command.BrakeSeconds),
            this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}