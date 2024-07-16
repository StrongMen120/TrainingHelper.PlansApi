

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.Domain.FakeData;
using Training.API.Plans.Core.Domain.Values;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class CreateTestEmployersUseCase : IUseCase<CreateTestEmployersUseCase.Input, CreateTestEmployersUseCase.IOutput>
{
    private readonly IUserCache checkUSerService;
    private readonly ITestEmployersRepository testEmployersRepository;
    
    public CreateTestEmployersUseCase(IUserCache checkUSerService, ITestEmployersRepository testEmployersRepository)
    {
        this.checkUSerService = checkUSerService;
        this.testEmployersRepository = testEmployersRepository;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var result = await inputPort.newEmployers.ToAsyncEnumerable().SelectAwait(async obj => await this.testEmployersRepository.CreateAsync(new ITestEmployersRepository.CreateTestEmployers(obj))).ToListAsync();
            outputPort.Success(result);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }
    public List<TestEmployersModel> TakeData (int count)
    {
        return TestEmployerFake.FakeListEmployersModel(count);
    }

    public record Input(
        List<TestEmployersModel> newEmployers
    ) : IInputPort;


    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<TestEmployersModel> testEmployers);
        public void UnknownError(Exception exception);
    }
}
