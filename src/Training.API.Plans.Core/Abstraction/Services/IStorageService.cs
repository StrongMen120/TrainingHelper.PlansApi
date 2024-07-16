using Microsoft.AspNetCore.Http;
using Training.API.Plans.Core.Domain;

namespace Training.API.Plans.Core.Abstraction.Services;

public interface IStorageService
{
    Task<ExercisesInfoDetailsModel> DownloadExerciseInfoDetailsAsync(ExercisesInfoModel Exercise);
    Task<FileDetailsModel> DownloadFileAsync(string Key, Guid ExerciseId);
    Task UploadFileAsync(IFormFile File, string Key);
    Task RemoveFileAsync(string Key);
}