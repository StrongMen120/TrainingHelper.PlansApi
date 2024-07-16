using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.TestEmployers.Update;

public class UpdateTestEmployersPresenter : ActionResultPresenterBase<UpdateTestEmployersUseCase>, UpdateTestEmployersUseCase.IOutput
{
    public UpdateTestEmployersPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void Success(IEnumerable<TestEmployersModel> testEmployers) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<IEnumerable<TestEmployersDto>>(testEmployers)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}