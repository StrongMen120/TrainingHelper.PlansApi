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

namespace Training.API.Plans.API.V1.Endpoints.Plans.Delete;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/plans")]
public sealed class PlansController : SimpleUseCaseController<DeletePlansUseCase, DeletePlansPresenter>
{
    public PlansController(ILogger logger, DeletePlansUseCase useCase, DeletePlansPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpDelete("{identifier}", Name = nameof(DeletePlans))]
    [SwaggerOperation(summary: "Delete plans")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(PlansDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> DeletePlans(long identifier)
    {
        await this.UseCase.Execute(new DeletePlansUseCase.Input(identifier), this.Presenter);
        return await this.Presenter.GetResultAsync();
    }
}