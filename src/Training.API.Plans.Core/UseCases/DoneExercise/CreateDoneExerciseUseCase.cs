using NodaTime;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class CreateDoneExerciseUseCase : IUseCase<CreateDoneExerciseUseCase.Input, CreateDoneExerciseUseCase.IOutput>
{
    private readonly IDoneExercisesRepository doneExercisesRepository;

    private readonly IExercisesInfoRepository exercisesInfoRepository;
    
    private readonly IUserCache checkUSerService;

    public CreateDoneExerciseUseCase(IDoneExercisesRepository doneExercisesRepository, IExercisesInfoRepository exercisesInfoRepository, IExercisesRecordsRepository exercisesRecordsRepository, IUserCache checkUSerService)
    {
        this.doneExercisesRepository = doneExercisesRepository;
        this.exercisesInfoRepository = exercisesInfoRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var exercise = await inputPort.DoneExercise.ToAsyncEnumerable().SelectAwait(async obj =>  await this.exercisesInfoRepository.FindAsync(new IExercisesInfoRepository.GetById(obj.ExerciseInfoId))).ToListAsync();
            
            if(CheckExercise(exercise))
            {
                outputPort.NotFoundExercise(inputPort);
                return;
            }
            var doneExercises = await inputPort.DoneExercise.ToAsyncEnumerable().SelectAwait(async obj =>  await this.doneExercisesRepository.CreateAsync(new IDoneExercisesRepository.CreateDoneExercises(inputPort.UserId, inputPort.Date, obj))).ToListAsync();
            outputPort.Success(doneExercises);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record Input(
        long UserId,
        LocalDate Date,
        IEnumerable<OneCreatedDoneExerciseModel> DoneExercise
    ) : IInputPort;


    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<DoneExercisesModel> doneExercises);
        public void NotFoundExercise(Input doneExercise);
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
