
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

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.Get;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/exercises-info")]
public sealed class ExercisesInfoController : SimpleUseCaseController<GetExerciseInfoUseCase, GetExerciseInfoPresenter>
{
    public ExercisesInfoController(ILogger logger, GetExerciseInfoUseCase useCase, GetExerciseInfoPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("{identifier}", Name = nameof(GetExerciseInfo))]
    [SwaggerOperation(summary: "Gets exercises info")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(ExercisesInfoDto), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetExerciseInfo(long identifier)
    {
        GetExerciseInfoUseCase.Input input = new(identifier);
        await this.UseCase.Execute(input, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}