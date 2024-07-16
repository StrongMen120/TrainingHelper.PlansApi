

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetAllExercisesInfoUseCase : IUseCase<GetAllExercisesInfoUseCase.IOutput>
{
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    
    private readonly IUserCache checkUSerService;
    public GetAllExercisesInfoUseCase(IExercisesInfoRepository exercisesInfoRepository, IUserCache checkUSerService)
    {
        this.exercisesInfoRepository = exercisesInfoRepository;
        this.checkUSerService = checkUSerService;
    }
    public async Task Execute(NullInputPort inputPort, IOutput outputPort)
    {
        try
        {
            var exercisesInfos = await this.exercisesInfoRepository.SearchAsync(new IExercisesInfoRepository.SearchAllExercisesInfo());
            outputPort.Success(exercisesInfos);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }
    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<ExercisesInfoModel> exercisesInfos);
        public void UnknownError(Exception exception);
    }
}