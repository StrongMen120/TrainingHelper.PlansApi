
using Microsoft.AspNetCore.Mvc;
using Training.Common.Hexagon.API.Controllers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Training.Common.Hexagon.Core;
using Training.API.Plans.API.V1.DTOs;
using Microsoft.AspNetCore.Authorization;
using Training.API.Plans.Core.UseCases;

namespace Training.API.Plans.API.V1.Endpoints.PlannedTraining.Get;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/planned-training")]
public sealed class PlannedTrainingController : SimpleUseCaseController<GetPlannedTrainingUseCase, GetPlannedTrainingPresenter>
{
    public PlannedTrainingController(ILogger logger, GetPlannedTrainingUseCase useCase, GetPlannedTrainingPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("{identifier}", Name = nameof(GetPlannedTraining))]
    [SwaggerOperation(summary: "Gets planned training")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(PlannedTrainingsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetPlannedTraining(Guid identifier)
    {
        await this.UseCase.Execute(new GetPlannedTrainingUseCase.Input(identifier), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}