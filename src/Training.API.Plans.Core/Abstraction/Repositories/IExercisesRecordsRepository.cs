
using NodaTime;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core.Repositories;

namespace Training.API.Plans.Core.Abstraction.Repositories;

public interface IExercisesRecordsRepository :
    ISearchRepository<IExercisesRecordsRepository.SearchAllExercisesBestRecordsToUser, ExercisesRecordsModel?>,
    ISearchRepository<IExercisesRecordsRepository.SearchAllExercisesRecordsToUser, ExercisesRecordsModel?>,
    ISearchRepository<IExercisesRecordsRepository.SearchExercisesStatistics, StatisticsModel>,
    ICreateRepository<IExercisesRecordsRepository.CreateNewExerciseRecords, ExercisesRecordsModel?>,
    ICreateRepository<IExercisesRecordsRepository.CreateNewExerciseRecordsFromDoneExercise, ExercisesRecordsModel?>
{   
    #region Gets


    #endregion Gets

    #region Searches

    public record class SearchAllExercisesBestRecordsToUser(long UserId);
    public record class SearchAllExercisesRecordsToUser(long ExerciseId, long UserId);
    public record class SearchExercisesStatistics(long ExerciseId, long UserId, int year, int month);

    #endregion Searches

    #region Crete & Updates

    public record class CreateNewExerciseRecords(long ExerciseId, long UserId, double Weight, int Reps, bool isAutomat = true);

    public record class CreateNewExerciseRecordsFromDoneExercise(long ExerciseId, long UserId, IEnumerable<double> Weight, IEnumerable<double> Reps);

    
    #endregion Crete & Updates

    #region Delete

    #endregion Delete
}
