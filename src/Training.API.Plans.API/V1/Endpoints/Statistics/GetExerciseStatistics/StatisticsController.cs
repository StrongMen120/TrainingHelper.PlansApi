
using Microsoft.AspNetCore.Mvc;
using Training.Common.Hexagon.API.Controllers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.UseCases;
using NodaTime;

namespace Training.API.Plans.API.V1.Endpoints.Statistics.GetExerciseStatistics;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/statistics")]
public sealed class StatisticsController : SimpleUseCaseController<GetExercisesStatisticsUseCase, GetExerciseStatisticsPresenter>
{
    public StatisticsController(ILogger logger, GetExercisesStatisticsUseCase useCase, GetExerciseStatisticsPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("exercises/{exerciseId}/user/{userId}/year/{year}/month/{month}", Name = nameof(GetExerciseStatistics))]
    [SwaggerOperation(summary: "Gets exercises statistics")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<StatisticsDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetExerciseStatistics(long exerciseId, long userId, int year, int month)
    {
        GetExercisesStatisticsUseCase.Input input = new(userId, exerciseId, year, month);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}