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

namespace Training.API.Plans.API.V1.Endpoints.PlannedTraining.Update;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/planned-training")]
public sealed class PlannedTrainingController : SimpleUseCaseController<UpdatePlannedTrainingUseCase, UpdatePlannedTrainingPresenter>
{
    public PlannedTrainingController(ILogger logger, UpdatePlannedTrainingUseCase useCase, UpdatePlannedTrainingPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPut(Name = nameof(UpdatePlannedTraining))]
    [SwaggerOperation(summary: "Update planned training")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(PlannedTrainingsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    public async ValueTask<IActionResult> UpdatePlannedTraining([FromBody] UpdatePlannedTrainingCommand command)
    {
        await this.UseCase.Execute(new UpdatePlannedTrainingUseCase.Input(command.Identifier,
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