namespace Training.API.Plans.Core.Domain;

public record RecordsModel(
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
    double OneRepetitionMaximum
);