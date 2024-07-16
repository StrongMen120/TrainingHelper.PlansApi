using Newtonsoft.Json;
using NodaTime;

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.Create;

public sealed record CreateDoneExerciseCommand
(
    [property: JsonProperty(Required = Required.Always)]
    long UserId,

    [property: JsonProperty(Required = Required.Always)]
    LocalDate Date,

    [property: JsonProperty(Required = Required.Always)]
    IEnumerable<OneCreatedDoneExerciseCommand> DoneExercise   
);

public sealed record OneCreatedDoneExerciseCommand(
    [property: JsonProperty(Required = Required.Always)]
    long ExerciseInfoId,
    
    [property: JsonProperty(Required = Required.Always)]
    int Series,
    
    [property: JsonProperty(Required = Required.Always)]
    IEnumerable<double> Reps,
    
    [property: JsonProperty(Required = Required.Always)]
    IEnumerable<double> Weight,
    
    [property: JsonProperty(Required = Required.Always)]
    int Rate,
    
    [property: JsonProperty(Required = Required.Always)]
    int RPE,
    
    [property: JsonProperty(Required = Required.Always)]
    int BrakeSeconds
);