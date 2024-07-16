

using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core;

namespace Training.API.Plans.Core.UseCases;

public class GetAttachmentExerciseInfoUseCase : IUseCase<GetAttachmentExerciseInfoUseCase.Input, GetAttachmentExerciseInfoUseCase.IOutput>
{
    private readonly IExercisesInfoRepository exercisesInfoRepository;
    private readonly IStorageService storageService;
    private readonly IUserCache checkUSerService;
    public GetAttachmentExerciseInfoUseCase(IExercisesInfoRepository exercisesInfoRepository, IUserCache checkUSerService, IStorageService storageService)
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
            var res = await this.GetsFilesAttachmentDetails(exerciseInfo);
            outputPort.Success(res);
        }
        catch (System.Exception ex)
        {
            outputPort.UnknownError(ex);
        }
    }
    private async Task<ExercisesInfoDetailsModel> GetsFilesAttachmentDetails(ExercisesInfoModel ExerciseInfo)
    {
        var files = await ExerciseInfo.Files.ToAsyncEnumerable().SelectAwait( async obj => await this.storageService.DownloadFileAsync(obj.PhotoId, obj.Identifier)).ToListAsync();
        return new ExercisesInfoDetailsModel(ExerciseInfo.Identifier, ExerciseInfo.Name, ExerciseInfo.Description, ExerciseInfo.AuthorId, ExerciseInfo.BodyElements, files, ExerciseInfo.CreatedAt, ExerciseInfo.CreatedBy, ExerciseInfo.ModifiedAt, ExerciseInfo.ModifiedBy);
    }

    public record struct Input(
        long ExerciseId
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(ExercisesInfoDetailsModel exercisesInfoDetails);
        public void NotFoundExercise(Input input);
        public void UnknownError(Exception exception);
    }
}
