using NodaTime;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.Core.Domain;

public record ExercisesRecordsModel(
    long Identifier,
    long Revision,
    long ExerciseId,
    long UserId,
    LocalDate Date,
    int Reps,
    double Weight,
    double LombardiResult,
    double BrzyckiResult,
    double EpleyResult,
    double MayhewResult,
    double AdamsResult,
    double BaechleResult,
    double BergerResult,
    double BrownResult,
    double OneRepetitionMaximum,
    bool isAutomat,
    LocalDateTime CreatedAt,
    UserDetailsModel CreatedBy,
    LocalDateTime? ModifiedAt,
    UserDetailsModel? ModifiedBy
);