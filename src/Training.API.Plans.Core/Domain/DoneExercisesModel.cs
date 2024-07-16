using NodaTime;

namespace Training.API.Plans.Core.Domain;

public record DoneExercisesModel(
    Guid Identifier,
    long UserId,
    long ExerciseInfoId,
    int Series,
    List<double> Reps,
    List<double> Weight,
    int Rate,
    int Rpe,
    int BrakeSeconds,
    ExercisesInfoModel Exercise,
    LocalDate Date,
    LocalDateTime? ModifiedAt,
    UserDetailsModel? ModifiedBy,
    LocalDateTime CreatedAt,
    UserDetailsModel CreatedBy
);