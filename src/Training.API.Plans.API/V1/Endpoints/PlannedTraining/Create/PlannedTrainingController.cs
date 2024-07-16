using Microsoft.AspNetCore.Mvc;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API.Controllers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Training.API.Plans.API.V1.DTOs;

namespace Training.API.Plans.API.V1.Endpoints.PlannedTraining.Create;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/planned-training")]
public sealed class PlannedTrainingController : SimpleUseCaseController<CreatePlannedTrainingUseCase, CreatePlannedTrainingPresenter>
{
    public PlannedTrainingController(ILogger logger, CreatePlannedTrainingUseCase useCase, CreatePlannedTrainingPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPost(Name = nameof(CreatePlannedTraining))]
    [SwaggerOperation(summary: "Create planned training")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(PlannedTrainingsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    public async ValueTask<IActionResult> CreatePlannedTraining([FromBody] CreatePlannedTrainingCommand command)
    {
        await this.UseCase.Execute(new CreatePlannedTrainingUseCase.Input(
            command.PlansId,
            command.PlansType,
            command.DateStart,
            command.DateEnd,
            command.UserId,
            command.TrainerId,
            command.GroupId), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}