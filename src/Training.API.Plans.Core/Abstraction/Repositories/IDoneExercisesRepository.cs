
using NodaTime;
using Training.API.Plans.Core.Domain;
using Training.Common.Hexagon.Core.Repositories;

namespace Training.API.Plans.Core.Abstraction.Repositories;

public interface IDoneExercisesRepository :
    IGetRepository<IDoneExercisesRepository.GetById, DoneExercisesModel?>,
    ISearchRepository<IDoneExercisesRepository.SearchDoneExerciseHistory, DoneExercisesModel>,
    ISearchRepository<IDoneExercisesRepository.SearchDoneExercisesAtDay, DoneExercisesModel>,
    ISearchRepository<IDoneExercisesRepository.SearchAllDoneExercises, DoneExercisesModel>,
    ICreateRepository<IDoneExercisesRepository.CreateDoneExercises, DoneExercisesModel>,
    IUpdateRepository<IDoneExercisesRepository.UpdateDoneExercises, DoneExercisesModel>,
    IDeleteRepository<IDoneExercisesRepository.DeleteDoneExercise, DoneExercisesModel>
{   
    #region Gets

    public record class GetById(Guid Identifier);

    #endregion Gets

    #region Searches

    public record class SearchDoneExerciseHistory(long UserId, long ExerciseId);
    public record class SearchDoneExercisesAtDay(LocalDate Date, long UserId);
    public record class SearchAllDoneExercises(long UserId);

    #endregion Searches

    #region Crete & Updates

    public record class CreateDoneExercises(long UserId, LocalDate Date, OneCreatedDoneExerciseModel createdDoneExercises);
    public record class UpdateDoneExercises(Guid Identifier, LocalDate Date, OneCreatedDoneExerciseModel createdDoneExercises);

    #endregion Crete & Updates

    #region Delete

    public record class DeleteDoneExercise(Guid Identifier);

    #endregion Delete
}
