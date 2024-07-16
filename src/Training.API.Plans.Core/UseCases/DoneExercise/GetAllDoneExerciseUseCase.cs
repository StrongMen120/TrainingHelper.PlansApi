

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetAllDoneExerciseUseCase : IUseCase<GetAllDoneExerciseUseCase.Input ,GetAllDoneExerciseUseCase.IOutput>
{
    private readonly IDoneExercisesRepository doneExercisesRepository;
    
    private readonly IUserCache checkUSerService;

    public GetAllDoneExerciseUseCase(IDoneExercisesRepository doneExercisesRepository, IUserCache checkUSerService)
    {
        this.doneExercisesRepository = doneExercisesRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var doneExercises = await this.doneExercisesRepository.SearchAsync(new IDoneExercisesRepository.SearchAllDoneExercises(inputPort.UserId));
            outputPort.Success(doneExercises);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record Input(
        long UserId
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<DoneExercisesModel> doneExercises);
        public void NotFoundUser(Input input);
        public void UnknownError(Exception exception);
    }
}