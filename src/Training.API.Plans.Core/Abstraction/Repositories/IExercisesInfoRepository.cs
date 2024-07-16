
using NodaTime;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core.Repositories;

namespace Training.API.Plans.Core.Abstraction.Repositories;

public interface IExercisesInfoRepository :
    IGetRepository<IExercisesInfoRepository.GetById, ExercisesInfoModel?>,
    IGetRepository<IExercisesInfoRepository.GetByName, ExercisesInfoModel?>,
    ISearchRepository<IExercisesInfoRepository.GetFilesByExerciseId, FileModel>,
    ISearchRepository<IExercisesInfoRepository.SearchAllExercisesInfo, ExercisesInfoModel>,
    ICreateRepository<IExercisesInfoRepository.CreateExerciseInfo, ExercisesInfoModel>,
    IUpdateRepository<IExercisesInfoRepository.UpdateExerciseInfo, ExercisesInfoModel>,
    IUpdateRepository<IExercisesInfoRepository.UpdateAttachmentExerciseInfo, ExercisesInfoModel>,
    IDeleteRepository<IExercisesInfoRepository.DeleteExerciseInfo, ExercisesInfoModel>,
    IDeleteRepository<IExercisesInfoRepository.DeleteAttachmentExerciseInfo, ExercisesInfoModel>
{   
    #region Gets

    public record class GetById(long ExerciseId);
    public record class GetByName(string Name);

    #endregion Gets

    #region Searches

    public record class GetFilesByExerciseId(long ExerciseId);
    public record class SearchAllExercisesInfo();

    #endregion Searches

    #region Crete & Updates

    public record class UpdateAttachmentExerciseInfo(long ExerciseId, string photoId);
    public record class CreateExerciseInfo(string Name, string Description, long AuthorId, IEnumerable<Domain.Values.BodyElements> BodyElements);
    public record class UpdateExerciseInfo(long ExerciseId, string Name, string Description, IEnumerable<Domain.Values.BodyElements> BodyElements);

    #endregion Crete & Updates

    #region Delete

    public record class DeleteExerciseInfo(long ExerciseId);
    public record class DeleteAttachmentExerciseInfo(long ExerciseId, string Key);

    #endregion Delete
}
