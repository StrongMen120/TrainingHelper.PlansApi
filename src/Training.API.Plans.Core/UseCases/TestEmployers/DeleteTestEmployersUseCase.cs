

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class DeleteTestEmployersUseCase : IUseCase<DeleteTestEmployersUseCase.Input, DeleteTestEmployersUseCase.IOutput>
{
    private readonly IUserCache checkUSerService;
    private readonly ITestEmployersRepository testEmployersRepository;

    public DeleteTestEmployersUseCase(IUserCache checkUSerService, ITestEmployersRepository testEmployersRepository)
    {
        this.checkUSerService = checkUSerService;
        this.testEmployersRepository = testEmployersRepository;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var result = await inputPort.deletedId.ToAsyncEnumerable().SelectAwait(async obj => await this.testEmployersRepository.DeleteByIdAsync(new ITestEmployersRepository.DeleteTestEmployers(obj))).ToListAsync();
            outputPort.Success(result);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }
    public async Task<List<long>> TakeData (int count)
    {
        var result = await this.testEmployersRepository.SearchAsync(new ITestEmployersRepository.SearchTestEmployers(count));
        if(result != null)
        {
            return result.Select(p => p.Id).ToList();
        }
        return new List<long>();
    }
    public record struct Input(
        List<long> deletedId
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<TestEmployersModel> testEmployers);
        public void UnknownError(Exception exception);
    }
}
