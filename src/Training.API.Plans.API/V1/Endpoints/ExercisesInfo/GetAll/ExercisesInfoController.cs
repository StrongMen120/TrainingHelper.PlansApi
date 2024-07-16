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

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.GetAll;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/exercises-info")]
public sealed class ExercisesInfoController : SimpleUseCaseController<GetAllExercisesInfoUseCase, GetAllExercisesInfoPresenter>
{
    public ExercisesInfoController(ILogger logger, GetAllExercisesInfoUseCase useCase, GetAllExercisesInfoPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet(Name = nameof(GetAllExercisesInfo))]
    [SwaggerOperation(summary: "Gets all exercises info")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<ExercisesInfoDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetAllExercisesInfo()
    {
        await this.UseCase.Execute(NullInputPort.Instance, this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}