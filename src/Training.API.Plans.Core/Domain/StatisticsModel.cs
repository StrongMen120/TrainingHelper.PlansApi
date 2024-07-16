using NodaTime;

namespace Training.API.Plans.Core.Domain;

public record StatisticsModel(
    long ExerciseId,
    long UserId,
    LocalDate Date,
    double WorkoutVolume,
    double WorkoutIntensity
);