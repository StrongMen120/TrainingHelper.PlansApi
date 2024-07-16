

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetAllUserPlannedTrainingUseCase : IUseCase<GetAllUserPlannedTrainingUseCase.Input, GetAllUserPlannedTrainingUseCase.IOutput>
{
    private readonly IPlansRepository plansRepository;
    private readonly IPlannedTrainingsRepository plannedTrainingsRepository;
    private readonly IUserCache checkUSerService;

    public GetAllUserPlannedTrainingUseCase(IPlannedTrainingsRepository plannedTrainingsRepository, IPlansRepository plansRepository, IUserCache checkUSerService)
    {
        this.plannedTrainingsRepository = plannedTrainingsRepository;
        this.plansRepository = plansRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var result = new List<PlannedTrainingsModel>();
            var user = await this.checkUSerService.CheckUserId(inputPort.UserId);
            if(user.Groups.Count() != 0)
            {
                foreach (var groupId in user.Groups)
                {
                    result.AddRange(await this.plannedTrainingsRepository.SearchAsync(new IPlannedTrainingsRepository.SearchAllTrainerPlannedTrainings(groupId)));    
                }
            }
            if(user.IsTrainer)
                result.AddRange(await this.plannedTrainingsRepository.SearchAsync(new IPlannedTrainingsRepository.SearchAllTrainerPlannedTrainings(user.Id)));
            result.AddRange(await this.plannedTrainingsRepository.SearchAsync(new IPlannedTrainingsRepository.SearchAllUserPlannedTrainings(inputPort.UserId)));
            outputPort.Success(result.Distinct());
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
        public void Success(IEnumerable<PlannedTrainingsModel> plannedTrainingsModel);
        public void UnknownError(Exception exception);
    }
}