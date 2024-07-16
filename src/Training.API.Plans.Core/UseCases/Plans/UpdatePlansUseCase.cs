

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.Domain.Values;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class UpdatePlansUseCase : IUseCase<UpdatePlansUseCase.Input, UpdatePlansUseCase.IOutput>
{
    private readonly IPlansRepository plansRepository;
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    private readonly IUserCache checkUSerService;

    public UpdatePlansUseCase(IExercisesInfoRepository exercisesInfoRepository, IPlansRepository plansRepository, IUserCache checkUSerService)
    {
        this.exercisesInfoRepository = exercisesInfoRepository;
        this.plansRepository = plansRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var exercise = await inputPort.PlannedExercise.ToAsyncEnumerable().SelectAwait(async obj =>  await this.exercisesInfoRepository.FindAsync(new IExercisesInfoRepository.GetById(obj.ExerciseInfoId))).ToListAsync();
            
            if(CheckExercise(exercise))
            {
                outputPort.NotFoundExercise(inputPort);
                return;
            }

            var plansIdentifier = await this.plansRepository.FindAsync(new IPlansRepository.GetById(inputPort.Identifier));
            if(plansIdentifier == default)
            {
                outputPort.NotFoundPlans(inputPort);
                return;
            }

            if(plansIdentifier.Name != inputPort.Name)
            {
                var plansName = await this.plansRepository.FindAsync(new IPlansRepository.GetByName(inputPort.Name));
                if(plansName != default)
                {
                    outputPort.DuplicateName(plansName);
                    return;
                }
            }

            var newPlans = await this.plansRepository.UpdateAsync(new IPlansRepository.UpdatePlans(inputPort.Identifier, inputPort.Name, inputPort.Description, inputPort.Image, inputPort.PlannedExercise));
            outputPort.Success(newPlans);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record Input(
        long Identifier,
        string Name,
        string Description,
        PlansImage Image,
        IEnumerable<OneUpdatedPlannedExerciseModel> PlannedExercise 
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(PlansModel plansModel);
        public void DuplicateName(PlansModel plansModel);
        public void NotFoundPlans(Input input);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
        
    private bool CheckExercise(List<ExercisesInfoModel> listExercisesInfo)
    {
        foreach (var exerciseInfo in listExercisesInfo)
        {
            if(exerciseInfo is null)
                return true;
        }
        return false;
    }
}
