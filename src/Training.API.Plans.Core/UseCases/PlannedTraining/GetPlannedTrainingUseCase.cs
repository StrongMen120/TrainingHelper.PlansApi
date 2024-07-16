

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetPlannedTrainingUseCase : IUseCase<GetPlannedTrainingUseCase.Input, GetPlannedTrainingUseCase.IOutput>
{
    private readonly IPlannedTrainingsRepository plannedTrainingsRepository;
    private readonly IUserCache checkUSerService;
    public GetPlannedTrainingUseCase(IPlannedTrainingsRepository plannedTrainingsRepository, IUserCache checkUSerService)
    {
        this.plannedTrainingsRepository = plannedTrainingsRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var plannedTraining = await this.plannedTrainingsRepository.GetAsync(new IPlannedTrainingsRepository.GetById(inputPort.Identifier));
            if(plannedTraining == default)
            {
                outputPort.NotFoundPlannedTraining(inputPort);
                return;
            }

            outputPort.Success(plannedTraining);
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
