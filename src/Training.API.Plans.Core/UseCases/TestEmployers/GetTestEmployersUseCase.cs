

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetTestEmployersUseCase : IUseCase<GetTestEmployersUseCase.Input, GetTestEmployersUseCase.IOutput>
{    
    private readonly IUserCache checkUSerService;
    private readonly ITestEmployersRepository testEmployersRepository;
    public GetTestEmployersUseCase(IUserCache checkUSerService, ITestEmployersRepository testEmployersRepository)
    {
        this.checkUSerService = checkUSerService;
        this.testEmployersRepository = testEmployersRepository;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var result = await this.testEmployersRepository.SearchAsync(new ITestEmployersRepository.SearchTestEmployers(inputPort.count));
            outputPort.Success(result);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        int count
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<TestEmployersModel> testEmployers);
        public void UnknownError(Exception exception);
    }
}
