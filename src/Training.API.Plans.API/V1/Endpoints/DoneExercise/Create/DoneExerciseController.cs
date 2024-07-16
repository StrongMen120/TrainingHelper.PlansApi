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

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.Create;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/done-exercises")]
public sealed class DoneExerciseController : SimpleUseCaseController<CreateDoneExerciseUseCase, CreateDoneExercisePresenter>
{
    public DoneExerciseController(ILogger logger, CreateDoneExerciseUseCase useCase, CreateDoneExercisePresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPost(Name = nameof(CreateDoneExercise))]
    [SwaggerOperation(summary: "Create done exercises")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<DoneExercisesDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> CreateDoneExercise([FromBody] CreateDoneExerciseCommand command)
    {
        await this.UseCase.Execute(new CreateDoneExerciseUseCase.Input(
            command.UserId,
            command.Date,
            command.DoneExercise.Select(obj => new Core.Domain.OneCreatedDoneExerciseModel(obj.ExerciseInfoId, obj.Series, obj.Reps, obj.Weight, obj.Rate, obj.RPE, obj.BrakeSeconds))),
            this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}