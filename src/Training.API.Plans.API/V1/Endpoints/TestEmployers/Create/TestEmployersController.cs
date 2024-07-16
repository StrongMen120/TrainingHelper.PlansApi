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
using Training.API.Plans.Core.Domain;
using System.Diagnostics;

namespace Training.API.Plans.API.V1.Endpoints.TestEmployers.Create;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/test/employers")]
public sealed class TestEmployersController : SimpleUseCaseController<CreateTestEmployersUseCase, CreateTestEmployersPresenter>
{
    public TestEmployersController(ILogger logger, CreateTestEmployersUseCase useCase, CreateTestEmployersPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPost("{count}", Name = nameof(CreateTestEmployers))]
    [SwaggerOperation(summary: "Create Test Employers")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<TestEmployersDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> CreateTestEmployers(int count)
    {
        Stopwatch stopwatch = new Stopwatch();
        var result = this.UseCase.TakeData(count);
        stopwatch.Start();
        await this.UseCase.Execute(new CreateTestEmployersUseCase.Input(result), this.Presenter);
        stopwatch.Stop();
        this.Logger.Information($"he duration of the operation Create(count:{count}) Timeout: {stopwatch.Elapsed}");
        return await this.Presenter.GetResultAsync();
    }
}