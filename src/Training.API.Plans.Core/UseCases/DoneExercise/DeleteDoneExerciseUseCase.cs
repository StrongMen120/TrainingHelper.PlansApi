

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class DeleteDoneExerciseUseCase : IUseCase<DeleteDoneExerciseUseCase.Input, DeleteDoneExerciseUseCase.IOutput>
{
    private readonly IDoneExercisesRepository doneExercisesRepository;
    private readonly IUserCache checkUSerService;
    public DeleteDoneExerciseUseCase(IDoneExercisesRepository doneExercisesRepository, IUserCache checkUSerService)
    {
        this.doneExercisesRepository = doneExercisesRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var exercisesInfos = await this.doneExercisesRepository.DeleteByIdAsync(new IDoneExercisesRepository.DeleteDoneExercise(inputPort.DoneExerciseId));
            outputPort.Success(exercisesInfos);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        Guid DoneExerciseId
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(DoneExercisesModel doneExercise);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
}
