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

namespace Training.API.Plans.API.V1.Endpoints.PlannedTraining.GetAll;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/planned-trainings")]
public sealed class PlannedTrainingController : SimpleUseCaseController<GetAllUserPlannedTrainingUseCase, GetAllUserPlannedTrainingPresenter>
{
    public PlannedTrainingController(ILogger logger, GetAllUserPlannedTrainingUseCase useCase, GetAllUserPlannedTrainingPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("{userId}", Name = nameof(GetAllUserPlannedTrainings))]
    [SwaggerOperation(summary: "Gets all user planned training")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<PlannedTrainingsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllUserPlannedTrainings(long userId)
    {
        await this.UseCase.Execute(new GetAllUserPlannedTrainingUseCase.Input(userId), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}