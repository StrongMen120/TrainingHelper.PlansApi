using NodaTime;

namespace Training.API.Plans.Core.Domain;

public record PlannedExercisesModel(
    Guid Identifier,
    long PlansId,
    long ExerciseInfoId,
    int Series,
    IEnumerable<double> Reps,
    IEnumerable<double> Weight,
    int Rate,
    int Rpe,
    int BrakeSeconds,
    ExercisesInfoModel Exercise,
    LocalDateTime? ModifiedAt,
    UserDetailsModel? ModifiedBy,
    LocalDateTime CreatedAt,
    UserDetailsModel CreatedBy
);