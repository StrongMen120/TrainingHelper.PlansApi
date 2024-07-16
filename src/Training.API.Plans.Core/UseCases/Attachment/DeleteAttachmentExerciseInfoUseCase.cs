

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class DeleteAttachmentExerciseInfoUseCase : IUseCase<DeleteAttachmentExerciseInfoUseCase.Input, DeleteAttachmentExerciseInfoUseCase.IOutput>
{
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    private readonly IStorageService storageService;
    private readonly IUserCache checkUSerService;
    public DeleteAttachmentExerciseInfoUseCase(IExercisesInfoRepository exercisesInfoRepository, IUserCache checkUSerService, IStorageService storageService)
    {
        this.exercisesInfoRepository = exercisesInfoRepository;
        this.checkUSerService = checkUSerService;
        this.storageService = storageService;
    }
    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var exerciseInfo = await this.exercisesInfoRepository.FindAsync(new IExercisesInfoRepository.GetById(inputPort.ExerciseId));
            if(exerciseInfo == default)
            {
                outputPort.NotFoundExercise(inputPort);
                return;
            }
            await this.storageService.RemoveFileAsync(inputPort.Key);
            var exercise = await this.exercisesInfoRepository.DeleteByIdAsync(new IExercisesInfoRepository.DeleteAttachmentExerciseInfo(inputPort.ExerciseId, inputPort.Key));
            var res = await this.storageService.DownloadExerciseInfoDetailsAsync(exercise);
            outputPort.Success(res);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        long ExerciseId,
        string Key
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(ExercisesInfoDetailsModel files);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
}
