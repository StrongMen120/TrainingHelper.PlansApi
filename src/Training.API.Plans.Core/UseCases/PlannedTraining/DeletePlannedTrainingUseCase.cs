

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class DeletePlannedTrainingUseCase : IUseCase<DeletePlannedTrainingUseCase.Input, DeletePlannedTrainingUseCase.IOutput>
{
    private readonly IPlannedTrainingsRepository plannedTrainingsRepository;
    private readonly IUserCache checkUSerService;

    public DeletePlannedTrainingUseCase(IPlannedTrainingsRepository plannedTrainingsRepository, IUserCache checkUSerService)
    {
        this.plannedTrainingsRepository = plannedTrainingsRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var plans = await this.plannedTrainingsRepository.GetAsync(new IPlannedTrainingsRepository.GetById(inputPort.Identifier));
            if(plans == default)
            {
                outputPort.NotFoundPlannedTraining(inputPort);
                return;
            }

            var deletedPlans = await this.plannedTrainingsRepository.DeleteByIdAsync(new IPlannedTrainingsRepository.DeletePlannedTrainings(inputPort.Identifier));
            outputPort.Success(deletedPlans);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        Guid Identifier
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(PlannedTrainingsModel plannedTrainingModel);
        public void NotFoundPlannedTraining(Input input);
        public void UnknownError(Exception exception);
    }
}
