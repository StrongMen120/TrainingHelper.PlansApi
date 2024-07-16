

using NodaTime;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.Domain.Values;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class CreatePlannedTrainingUseCase : IUseCase<CreatePlannedTrainingUseCase.Input, CreatePlannedTrainingUseCase.IOutput>
{
    private readonly IPlansRepository plansRepository;
    private readonly IPlannedTrainingsRepository plannedTrainingsRepository;
    private readonly IUserCache checkUSerService;
    
    public CreatePlannedTrainingUseCase(IPlansRepository plansRepository, IPlannedTrainingsRepository plannedTrainingsRepository, IUserCache checkUSerService)
    {
        this.plansRepository = plansRepository;
        this.plannedTrainingsRepository = plannedTrainingsRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var plans = await this.plansRepository.FindAsync(new IPlansRepository.GetById(inputPort.PlansId));
            if(plans == default)
            {
                outputPort.NotFoundPlan(inputPort);
                return;
            }
            var plannedTraining = await this.plannedTrainingsRepository.CreateAsync(new IPlannedTrainingsRepository.CreatePlannedTrainings(inputPort.PlansId, inputPort.PlansType, inputPort.DateStart, inputPort.DateEnd, inputPort.UserId, inputPort.TrainerId, inputPort.GroupId));
            outputPort.Success(plannedTraining);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record Input(
        long PlansId,
        PlansType PlansType,
        LocalDateTime DateStart,
        LocalDateTime DateEnd,
        long? UserId,
        long? TrainerId,
        long? GroupId
    ) : IInputPort;


    public interface IOutput : IOutputPort
    {
        public void Success(PlannedTrainingsModel plannedTrainingModel);
        public void NotFoundPlan(Input input);
        public void UnknownError(Exception exception);
    }
}
