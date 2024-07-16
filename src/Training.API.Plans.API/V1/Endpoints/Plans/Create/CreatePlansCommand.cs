using Newtonsoft.Json;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.API.V1.Endpoints.Plans.Create;

public sealed record CreatePlansCommand
(
    [property: JsonProperty(Required = Required.Always)]
    string Name,
    
    [property: JsonProperty(Required = Required.Always)]
    string Description,
    
    [property: JsonProperty(Required = Required.Always)]
    PlansImage Image,
    
    [property: JsonProperty(Required = Required.Always)]
    long AuthorId,

    [property: JsonProperty(Required = Required.Always)]
    IEnumerable<OneCreatedPlannedExerciseCommand> PlannedExercise   
);

public sealed record OneCreatedPlannedExerciseCommand(
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
