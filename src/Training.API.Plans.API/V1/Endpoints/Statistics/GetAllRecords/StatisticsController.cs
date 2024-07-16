
using Microsoft.AspNetCore.Mvc;
using Training.Common.Hexagon.API.Controllers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.UseCases;

namespace Training.API.Plans.API.V1.Endpoints.Statistics.GetAllRecords;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/statistics")]
public sealed class StatisticsController : SimpleUseCaseController<GetExercisesAllRecordsUseCase, GetExercisesAllRecordsPresenter>
{
    public StatisticsController(ILogger logger, GetExercisesAllRecordsUseCase useCase, GetExercisesAllRecordsPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("exercises-records/{userId}", Name = nameof(GetExercisesAllRecords))]
    [SwaggerOperation(summary: "Gets exercises all records to user")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<ExercisesRecordsDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetExercisesAllRecords(long userId)
    {
        GetExercisesAllRecordsUseCase.Input input = new(userId);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}