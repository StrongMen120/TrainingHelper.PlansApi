

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.Domain.FakeData;
using Training.API.Plans.Core.Domain.Values;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class UpdateTestEmployersUseCase : IUseCase<UpdateTestEmployersUseCase.Input, UpdateTestEmployersUseCase.IOutput>
{
    private readonly IUserCache checkUSerService;
    private readonly ITestEmployersRepository testEmployersRepository;

    public UpdateTestEmployersUseCase(IUserCache checkUSerService, ITestEmployersRepository testEmployersRepository)
    {
        this.checkUSerService = checkUSerService;
        this.testEmployersRepository = testEmployersRepository;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var result = await inputPort.updatedTestEmployers.ToAsyncEnumerable().SelectAwait(async obj => await this.testEmployersRepository.UpdateAsync(new ITestEmployersRepository.UpdateTestEmployers(obj))).ToListAsync();
            outputPort.Success(result);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }
    
    public async Task<List<TestEmployersModel>> TakeData (int count)
    {
        var newRecords =  TestEmployerFake.FakeListEmployersModel(count);
        var oldRecords = await this.testEmployersRepository.SearchAsync(new ITestEmployersRepository.SearchTestEmployers(count));
        var result = new List<TestEmployersModel>();
        if(oldRecords != null && newRecords != null && newRecords.Count == oldRecords.Count())
        {
            var x = oldRecords.ToList();
            for (int i = 0; i < newRecords.Count; i++)
            {
                result.Add(newRecords[i] with {Id = x[i].Id});
            }
        }
        return result;
    }

    public record Input(
        List<TestEmployersModel> updatedTestEmployers
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<TestEmployersModel> testEmployers);
        public void UnknownError(Exception exception);
    }
}
