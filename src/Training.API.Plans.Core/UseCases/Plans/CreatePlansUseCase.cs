

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.Domain.Values;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class CreatePlansUseCase : IUseCase<CreatePlansUseCase.Input, CreatePlansUseCase.IOutput>
{
    private readonly IPlansRepository plansRepository;
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    private readonly IUserCache checkUSerService;
    
    public CreatePlansUseCase(IPlansRepository plansRepository, IExercisesInfoRepository exercisesInfoRepository, IUserCache checkUSerService)
    {
        this.plansRepository = plansRepository;
        this.exercisesInfoRepository = exercisesInfoRepository;
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

            var plans = await this.plansRepository.FindAsync(new IPlansRepository.GetByName(inputPort.Name));
            if(plans != default)
            {
                outputPort.DuplicateName(plans);
                return;
            }

            var newPlans = await this.plansRepository.CreateAsync(new IPlansRepository.CreatePlans(inputPort.Name, inputPort.Description, inputPort.Image, inputPort.AuthorId, inputPort.PlannedExercise));
            outputPort.Success(newPlans);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record Input(
        string Name,
        string Description,
        PlansImage Image,
        long AuthorId,
        IEnumerable<OneCreatedPlannedExerciseModel> PlannedExercise
    ) : IInputPort;


    public interface IOutput : IOutputPort
    {
        public void Success(PlansModel plansModel);
        public void DuplicateName(PlansModel plansModel);
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
