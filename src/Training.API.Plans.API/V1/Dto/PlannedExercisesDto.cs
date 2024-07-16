using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NodaTime;

namespace Training.API.Plans.API.V1.DTOs;

[ApiVersion("1")]
public record PlannedExercisesDto(
    [property: JsonProperty(Required = Required.Always)]
    Guid Identifier,
    
    [property: JsonProperty(Required = Required.Always)]
    long PlansId,
    
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
    int BrakeSeconds,
    
    [property: JsonProperty(Required = Required.Always)]
    ExercisesInfoDto ExerciseInfo,

    [property: JsonProperty(Required = Required.Always)]
    LocalDateTime CreatedAt,

    [property: JsonProperty(Required = Required.Always)]
    UserDetailsDto CreatedBy,

    [property: JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    LocalDateTime? ModifiedAt,
    
    [property: JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    UserDetailsDto? ModifiedBy
);
