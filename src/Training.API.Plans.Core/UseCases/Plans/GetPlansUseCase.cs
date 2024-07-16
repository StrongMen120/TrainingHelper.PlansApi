

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetPlansUseCase : IUseCase<GetPlansUseCase.Input, GetPlansUseCase.IOutput>
{
    private readonly IPlansRepository plansRepository;
    
    private readonly IUserCache checkUSerService;
    public GetPlansUseCase(IPlansRepository plansRepository, IUserCache checkUSerService)
    {
        this.plansRepository = plansRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var plans = await this.plansRepository.GetAsync(new IPlansRepository.GetById(inputPort.Identifier));
            if(plans == default)
            {
                outputPort.NotFoundPlan(inputPort);
                return;
            }

            outputPort.Success(plans);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        long Identifier
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(PlansModel plans);
        public void NotFoundPlan(Input input);
        public void UnknownError(Exception exception);
    }
}
