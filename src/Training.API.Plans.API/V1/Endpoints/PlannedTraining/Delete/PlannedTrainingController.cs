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

namespace Training.API.Plans.API.V1.Endpoints.PlannedTraining.Delete;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/planned-training")]
public sealed class PlannedTrainingController : SimpleUseCaseController<DeletePlannedTrainingUseCase, DeletePlannedTrainingPresenter>
{
    public PlannedTrainingController(ILogger logger, DeletePlannedTrainingUseCase useCase, DeletePlannedTrainingPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpDelete("{identifier}", Name = nameof(DeletePlannedTraining))]
    [SwaggerOperation(summary: "Delete planned trainings")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(PlannedTrainingsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> DeletePlannedTraining(Guid identifier)
    {
        await this.UseCase.Execute(new DeletePlannedTrainingUseCase.Input(identifier), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}