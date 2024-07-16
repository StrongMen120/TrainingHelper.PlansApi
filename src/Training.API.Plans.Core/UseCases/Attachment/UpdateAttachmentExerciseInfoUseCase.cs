

using Microsoft.AspNetCore.Http;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class UpdateAttachmentExerciseInfoUseCase : IUseCase<UpdateAttachmentExerciseInfoUseCase.Input, UpdateAttachmentExerciseInfoUseCase.IOutput>
{
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    private readonly IStorageService storageService;
    private readonly IUserCache checkUSerService;
    public UpdateAttachmentExerciseInfoUseCase(IExercisesInfoRepository exercisesInfoRepository, IUserCache checkUSerService, IStorageService storageService)
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
                
            string photoId = $"{inputPort.Photo.FileName}-EXI{exerciseInfo.Identifier}" ;
            await this.storageService.UploadFileAsync(inputPort.Photo, photoId);
            
            var exercise = await this.exercisesInfoRepository.UpdateAsync(new IExercisesInfoRepository.UpdateAttachmentExerciseInfo(exerciseInfo.Identifier, photoId));
            outputPort.Success(await this.storageService.DownloadExerciseInfoDetailsAsync(exercise));
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }

    public record struct Input(
        long ExerciseId,
        IFormFile Photo
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(ExercisesInfoDetailsModel exercise);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
}
