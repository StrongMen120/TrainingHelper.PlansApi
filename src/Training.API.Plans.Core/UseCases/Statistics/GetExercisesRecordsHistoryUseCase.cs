using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetExercisesRecordsHistoryUseCase : IUseCase<GetExercisesRecordsHistoryUseCase.Input, GetExercisesRecordsHistoryUseCase.IOutput>
{
    private readonly IExercisesRecordsRepository exercisesRecordsRepository;
    
    private readonly IUserCache checkUSerService;
    public GetExercisesRecordsHistoryUseCase(IExercisesRecordsRepository exercisesRecordsRepository, IUserCache checkUSerService)
    {
        this.exercisesRecordsRepository = exercisesRecordsRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var exercisesRecords = await this.exercisesRecordsRepository.SearchAsync(new IExercisesRecordsRepository.SearchAllExercisesRecordsToUser(inputPort.ExerciseId, inputPort.UserId));
            if(exercisesRecords == default)
            {
                outputPort.NotFoundExerciseRecords(inputPort);
                return;
            }
            outputPort.Success(exercisesRecords);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        long UserId,
        long ExerciseId
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<ExercisesRecordsModel> exercisesRecords);
        public void NotFoundExerciseRecords(Input input);
        public void UnknownError(Exception exception);
    }
}
