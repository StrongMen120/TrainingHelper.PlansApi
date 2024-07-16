

using NodaTime;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetExercisesStatisticsUseCase : IUseCase<GetExercisesStatisticsUseCase.Input, GetExercisesStatisticsUseCase.IOutput>
{
    private readonly IExercisesRecordsRepository exercisesRecordsRepository;
    private readonly IDoneExercisesRepository doneExercisesRepository;
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    private readonly IUserCache checkUSerService;
    public GetExercisesStatisticsUseCase(IExercisesRecordsRepository exercisesRecordsRepository, IDoneExercisesRepository doneExercisesRepository, IExercisesInfoRepository exercisesInfoRepository, IUserCache checkUSerService)
    {
        this.exercisesRecordsRepository = exercisesRecordsRepository;
        this.doneExercisesRepository = doneExercisesRepository;
        this.exercisesInfoRepository = exercisesInfoRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var exercise =  await this.exercisesInfoRepository.FindAsync(new IExercisesInfoRepository.GetById(inputPort.ExerciseId));
            if(exercise == null) 
            {
                outputPort.NotFoundExercise(inputPort);
                return;
            }
            var result = await this.exercisesRecordsRepository.SearchAsync(new IExercisesRecordsRepository.SearchExercisesStatistics(inputPort.ExerciseId, inputPort.UserId, inputPort.Year, inputPort.Month));
            outputPort.Success(result);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        long UserId,
        long ExerciseId,
        int Year,
        int Month
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<StatisticsModel> statistics);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
}
