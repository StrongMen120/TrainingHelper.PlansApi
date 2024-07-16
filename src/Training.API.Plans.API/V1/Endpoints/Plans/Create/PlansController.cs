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
using Training.API.Plans.Core.Domain;

namespace Training.API.Plans.API.V1.Endpoints.Plans.Create;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/plans")]
public sealed class PlansController : SimpleUseCaseController<CreatePlansUseCase, CreatePlansPresenter>
{
    public PlansController(ILogger logger, CreatePlansUseCase useCase, CreatePlansPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPost(Name = nameof(CreatePlans))]
    [SwaggerOperation(summary: "Create plans")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(PlansDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    public async ValueTask<IActionResult> CreatePlans([FromBody] CreatePlansCommand command)
    {
        await this.UseCase.Execute(new CreatePlansUseCase.Input(
            command.Name,
            command.Description,
            command.Image,
            command.AuthorId,
            command.PlannedExercise.Select(e => new OneCreatedPlannedExerciseModel(e.ExerciseInfoId, e.Series, e.Reps, e.Weight, e.Rate, e.RPE, e.BrakeSeconds))
        ), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}