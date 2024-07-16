

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetExerciseInfoUseCase : IUseCase<GetExerciseInfoUseCase.Input, GetExerciseInfoUseCase.IOutput>
{
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    
    private readonly IUserCache checkUSerService;
    public GetExerciseInfoUseCase(IExercisesInfoRepository exercisesInfoRepository, IUserCache checkUSerService)
    {
        this.exercisesInfoRepository = exercisesInfoRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var exercisesInfos = await this.exercisesInfoRepository.FindAsync(new IExercisesInfoRepository.GetById(inputPort.ExerciseId));
            if(exercisesInfos == default)
            {
                outputPort.NotFoundExercise(inputPort);
                return;
            }
            outputPort.Success(exercisesInfos);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        long ExerciseId
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(ExercisesInfoModel exerciseInfo);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
}
