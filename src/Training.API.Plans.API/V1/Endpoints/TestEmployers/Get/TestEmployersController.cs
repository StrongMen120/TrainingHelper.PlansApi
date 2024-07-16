
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
using System.Diagnostics;

namespace Training.API.Plans.API.V1.Endpoints.TestEmployers.Get;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/test/employers")]
public sealed class TestEmployersController : SimpleUseCaseController<GetTestEmployersUseCase, GetTestEmployersPresenter>
{
    public TestEmployersController(ILogger logger, GetTestEmployersUseCase useCase, GetTestEmployersPresenter presenter)
        : base(logger, useCase, presenter)
    { }
    

    [HttpGet("{count}", Name = nameof(GetTestEmployers))]
    [SwaggerOperation(summary: "Gets TestEmployers")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<TestEmployersDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetTestEmployers(int count)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        await this.UseCase.Execute(new GetTestEmployersUseCase.Input(count), this.Presenter);
        stopwatch.Stop();
        this.Logger.Information($"he duration of the operation Get(count:{count}) Timeout: {stopwatch.Elapsed}");
        return await this.Presenter.GetResultAsync();
    }
}