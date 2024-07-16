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

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.GetAll;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/done-exercises")]
public sealed class DoneExerciseController : SimpleUseCaseController<GetAllDoneExerciseUseCase, GetAllDoneExercisePresenter>
{
    public DoneExerciseController(ILogger logger, GetAllDoneExerciseUseCase useCase, GetAllDoneExercisePresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("{userId}", Name = nameof(GetAllDoneExercise))]
    [SwaggerOperation(summary: "Gets all done exercises")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<DoneExercisesDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllDoneExercise(long userId)
    {
        await this.UseCase.Execute(new GetAllDoneExerciseUseCase.Input(userId), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}