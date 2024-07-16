using Newtonsoft.Json;
using NodaTime;
using Swashbuckle.AspNetCore.Annotations;

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.Update;

public sealed record UpdateDoneExerciseCommand
(
    [property: JsonProperty(Required = Required.Always)]
    LocalDate Date,
    
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
