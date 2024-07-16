using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.TestEmployers.Delete;

public class DeleteTestEmployersPresenter : ActionResultPresenterBase<DeleteTestEmployersUseCase>, DeleteTestEmployersUseCase.IOutput
{
    public DeleteTestEmployersPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void Success(IEnumerable<TestEmployersModel> testEmployers) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<IEnumerable<TestEmployersDto>>(testEmployers)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}