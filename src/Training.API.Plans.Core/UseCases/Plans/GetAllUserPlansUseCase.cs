

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetAllUserPlansUseCase : IUseCase<GetAllUserPlansUseCase.Input, GetAllUserPlansUseCase.IOutput>
{
    private readonly IPlansRepository plansRepository;
    
    private readonly IUserCache checkUSerService;
    public GetAllUserPlansUseCase(IPlansRepository plansRepository, IUserCache checkUSerService)
    {
        this.plansRepository = plansRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var plans = await this.plansRepository.SearchAsync(new IPlansRepository.SearchAllPlans(inputPort.UserId));
            outputPort.Success(plans);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }
    
    public record Input(
        long UserId
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<PlansModel> plans);
        public void UnknownError(Exception exception);
    }
}