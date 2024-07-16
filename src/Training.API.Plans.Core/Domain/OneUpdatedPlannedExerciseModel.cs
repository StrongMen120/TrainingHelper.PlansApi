using NodaTime;

namespace Training.API.Plans.Core.Domain;

public record OneUpdatedPlannedExerciseModel(
    long ExerciseInfoId,
    int Series,
    IEnumerable<double> Reps,
    IEnumerable<double> Weight,
    int Rate,
    int Rpe,
    int BrakeSeconds
);