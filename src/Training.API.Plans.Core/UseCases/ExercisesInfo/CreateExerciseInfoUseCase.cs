

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class CreateExerciseInfoUseCase : IUseCase<CreateExerciseInfoUseCase.Input, CreateExerciseInfoUseCase.IOutput>
{
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    
    private readonly IUserCache checkUSerService;
    public CreateExerciseInfoUseCase(IExercisesInfoRepository exercisesInfoRepository, IUserCache checkUSerService)
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
            var newExerciseInfo = await this.exercisesInfoRepository.CreateAsync(new IExercisesInfoRepository.CreateExerciseInfo(inputPort.Name, inputPort.Description, inputPort.AuthorId, inputPort.BodyElements));
            outputPort.Success(newExerciseInfo);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record Input(
        string Name,
        string Description,
        long AuthorId,
        IEnumerable<Domain.Values.BodyElements> BodyElements
    ) : IInputPort;


    public interface IOutput : IOutputPort
    {
        public void Success(ExercisesInfoModel exerciseInfo);
        public void DuplicateName(ExercisesInfoModel exerciseInfo);
        public void UnknownError(Exception exception);
    }
}
