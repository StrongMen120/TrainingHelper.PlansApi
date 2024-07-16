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

namespace Training.API.Plans.API.V1.Endpoints.Statistics.Create;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/statistics")]
public sealed class StatisticsController : SimpleUseCaseController<CreateExercisesRecordsUseCase, CreateExercisesRecordsPresenter>
{
    public StatisticsController(ILogger logger, CreateExercisesRecordsUseCase useCase, CreateExercisesRecordsPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPost("exercises-records", Name = nameof(CreateExercisesRecords))]
    [SwaggerOperation(summary: "Create exercise records")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(ExercisesRecordsDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> CreateExercisesRecords([FromBody] CreateExercisesRecordsCommand command)
    {
        CreateExercisesRecordsUseCase.Input input = new(command.ExerciseId, command.UserId, command.Weight, command.Reps);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}