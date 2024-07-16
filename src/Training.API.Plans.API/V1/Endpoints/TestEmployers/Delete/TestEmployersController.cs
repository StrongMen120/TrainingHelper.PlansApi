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
using System.Diagnostics;

namespace Training.API.Plans.API.V1.Endpoints.TestEmployers.Delete;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/test/employers")]
public sealed class TestEmployersController : SimpleUseCaseController<DeleteTestEmployersUseCase, DeleteTestEmployersPresenter>
{
    public TestEmployersController(ILogger logger, DeleteTestEmployersUseCase useCase, DeleteTestEmployersPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpDelete("{count}", Name = nameof(DeleteTestEmployers))]
    [SwaggerOperation(summary: "Delete TestEmployers")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<TestEmployersDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> DeleteTestEmployers(int count)
    {
        Stopwatch stopwatch = new Stopwatch();
        var result = await this.UseCase.TakeData(count);
        stopwatch.Start();
        await this.UseCase.Execute(new DeleteTestEmployersUseCase.Input(result), this.Presenter);
        stopwatch.Stop();
        this.Logger.Information($"he duration of the operation Delete(count:{count}) Timeout: {stopwatch.Elapsed}");
        return await this.Presenter.GetResultAsync();
    }
}