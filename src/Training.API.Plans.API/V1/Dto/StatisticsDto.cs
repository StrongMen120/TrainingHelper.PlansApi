using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NodaTime;

namespace Training.API.Plans.API.V1.DTOs;

[ApiVersion("1")]
public record StatisticsDto(
    [property: JsonProperty(Required = Required.Always)]
    Guid Identifier,

    [property: JsonProperty(Required = Required.Always)]
    long ExerciseId,

    [property: JsonProperty(Required = Required.Always)]
    long UserId,
    
    [property: JsonProperty(Required = Required.Always)]
    LocalDate Date,

    [property: JsonProperty(Required = Required.Always)]
    double WorkoutVolume,

    [property: JsonProperty(Required = Required.Always)]
    double WorkoutIntensity
);
