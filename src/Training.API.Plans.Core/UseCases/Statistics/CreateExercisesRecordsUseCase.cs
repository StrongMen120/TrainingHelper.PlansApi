

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class CreateExercisesRecordsUseCase : IUseCase<CreateExercisesRecordsUseCase.Input, CreateExercisesRecordsUseCase.IOutput>
{
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    private readonly IExercisesRecordsRepository exercisesRecordsRepository;
    
    private readonly IUserCache checkUSerService;
    public CreateExercisesRecordsUseCase(IExercisesInfoRepository exercisesInfoRepository, IExercisesRecordsRepository exercisesRecordsRepository, IUserCache checkUSerService)
    {
        this.exercisesInfoRepository = exercisesInfoRepository;
        this.exercisesRecordsRepository = exercisesRecordsRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var exercisesRecords = await this.exercisesInfoRepository.FindAsync(new IExercisesInfoRepository.GetById(inputPort.ExerciseId));
            if(exercisesRecords == default)
            {
                outputPort.NotFoundExercise(inputPort);
                return;
            }
            var result = await this.exercisesRecordsRepository.CreateAsync(new IExercisesRecordsRepository.CreateNewExerciseRecords(inputPort.ExerciseId, inputPort.UserId, inputPort.Weight, inputPort.Reps, false));
            outputPort.Success(result);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record Input(
        long ExerciseId,
        long UserId,
        double Weight,
        int Reps
    ) : IInputPort;


    public interface IOutput : IOutputPort
    {
        public void Success(ExercisesRecordsModel exercisesRecords);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
}
