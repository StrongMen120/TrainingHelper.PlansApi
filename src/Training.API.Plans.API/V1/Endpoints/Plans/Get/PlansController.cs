
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

namespace Training.API.Plans.API.V1.Endpoints.Plans.Get;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/plan")]
public sealed class PlansController : SimpleUseCaseController<GetPlansUseCase, GetPlansPresenter>
{
    public PlansController(ILogger logger, GetPlansUseCase useCase, GetPlansPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("{identifier}", Name = nameof(GetPlan))]
    [SwaggerOperation(summary: "Gets plans")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(PlansDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetPlan(long identifier)
    {
        await this.UseCase.Execute(new GetPlansUseCase.Input(identifier), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}