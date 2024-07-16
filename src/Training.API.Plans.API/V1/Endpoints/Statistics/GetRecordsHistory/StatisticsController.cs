using Microsoft.AspNetCore.Mvc;
using Training.Common.Hexagon.API.Controllers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.UseCases;

namespace Training.API.Plans.API.V1.Endpoints.Statistics.GetRecordsHistory;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/statistics")]
public sealed class StatisticsController : SimpleUseCaseController<GetExercisesRecordsHistoryUseCase, GetExercisesRecordsHistoryPresenter>
{
    public StatisticsController(ILogger logger, GetExercisesRecordsHistoryUseCase useCase, GetExercisesRecordsHistoryPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("exercises-records/{exerciseId}/user/{userId}", Name = nameof(GetExercisesRecordsHistory))]
    [SwaggerOperation(summary: "Gets exercises records history")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<ExercisesRecordsDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetExercisesRecordsHistory(long exerciseId, long userId)
    {
        GetExercisesRecordsHistoryUseCase.Input input = new(userId, exerciseId);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}