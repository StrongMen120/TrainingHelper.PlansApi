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

namespace Training.API.Plans.API.V1.Endpoints.Plans.GetAll;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/plans")]
public sealed class PlansController : SimpleUseCaseController<GetAllUserPlansUseCase, GetAllUserPlansPresenter>
{
    public PlansController(ILogger logger, GetAllUserPlansUseCase useCase, GetAllUserPlansPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("{userId}",Name = nameof(GetAllUserPlans))]
    [SwaggerOperation(summary: "Gets all user plans")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<PlansDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllUserPlans(long userId)
    {
        await this.UseCase.Execute(new GetAllUserPlansUseCase.Input(userId), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}