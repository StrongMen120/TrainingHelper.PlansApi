using Newtonsoft.Json;

namespace Training.API.Plans.API.V1.Endpoints.Statistics.Create;

public sealed record CreateExercisesRecordsCommand
(
    [property: JsonProperty(Required = Required.Always)]
    long ExerciseId,
    
    [property: JsonProperty(Required = Required.Always)]
    long UserId,

    [property: JsonProperty(Required = Required.Always)]
    int Reps,

    [property: JsonProperty(Required = Required.Always)]
    double Weight
);
