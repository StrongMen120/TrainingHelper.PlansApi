

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class UpdateExerciseInfoUseCase : IUseCase<UpdateExerciseInfoUseCase.Input, UpdateExerciseInfoUseCase.IOutput>
{
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    
    private readonly IUserCache checkUSerService;
    public UpdateExerciseInfoUseCase(IExercisesInfoRepository exercisesInfoRepository, IUserCache checkUSerService)
    {
        this.exercisesInfoRepository = exercisesInfoRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var checkExerciseInfoName = await this.exercisesInfoRepository.FindAsync(new IExercisesInfoRepository.GetByName(inputPort.Name));
            if(checkExerciseInfoName != null) 
            {
                outputPort.DuplicateName(checkExerciseInfoName);
                return;
            }

            var exercisesInfos = await this.exercisesInfoRepository.UpdateAsync(new IExercisesInfoRepository.UpdateExerciseInfo(inputPort.ExerciseId, inputPort.Name, inputPort.Description, inputPort.BodyElements));
            if(exercisesInfos == null) 
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

    public record Input(
        long ExerciseId,
        string Name,
        string Description,
        IEnumerable<Domain.Values.BodyElements> BodyElements
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(ExercisesInfoModel exerciseInfo);
        public void DuplicateName(ExercisesInfoModel exerciseInfo);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
}
