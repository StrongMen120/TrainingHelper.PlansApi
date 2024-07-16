

using NodaTime;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class UpdateDoneExerciseUseCase : IUseCase<UpdateDoneExerciseUseCase.Input, UpdateDoneExerciseUseCase.IOutput>
{
    private readonly IDoneExercisesRepository doneExercisesRepository;
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    
    private readonly IUserCache checkUSerService;
    public UpdateDoneExerciseUseCase(IExercisesInfoRepository exercisesInfoRepository, IDoneExercisesRepository doneExercisesRepository, IUserCache checkUSerService)
    {
        this.exercisesInfoRepository = exercisesInfoRepository;
        this.doneExercisesRepository = doneExercisesRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var exercisesInfos = await this.exercisesInfoRepository.GetAsync(new IExercisesInfoRepository.GetById(inputPort.ExerciseInfoId));
            if(exercisesInfos == null)
            {
                outputPort.NotFoundExercise(inputPort);
                return;
            }
            
            var updatedDoneExercises = await this.doneExercisesRepository.UpdateAsync(new IDoneExercisesRepository.UpdateDoneExercises(inputPort.Identifier, inputPort.Data,
                new(inputPort.ExerciseInfoId, inputPort.Series, inputPort.Reps, inputPort.Weight, inputPort.Rate, inputPort.RPE, inputPort.BrakeSeconds)));
                
            outputPort.Success(updatedDoneExercises);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        Guid Identifier,
        LocalDate Data,
        long ExerciseInfoId,
        int Series,
        IEnumerable<double> Reps,
        IEnumerable<double> Weight,
        int Rate,
        int RPE,
        int BrakeSeconds 
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(DoneExercisesModel doneExercises);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
}
