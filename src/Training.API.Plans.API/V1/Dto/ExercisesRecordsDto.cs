using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NodaTime;

namespace Training.API.Plans.API.V1.DTOs;

[ApiVersion("1")]
public record ExercisesRecordsDto(
    [property: JsonProperty(Required = Required.Always)]
    long Identifier,

    [property: JsonProperty(Required = Required.Always)]
    long Revision,

    [property: JsonProperty(Required = Required.Always)]
    long ExerciseId,
    
    [property: JsonProperty(Required = Required.Always)]
    long UserId,
    
    [property: JsonProperty(Required = Required.Always)]
    LocalDate Date,

    [property: JsonProperty(Required = Required.Always)]
    double Reps,

    [property: JsonProperty(Required = Required.Always)]
    double Weight,

    [property: JsonProperty(Required = Required.Always)]
    double LombardiResult,

    [property: JsonProperty(Required = Required.Always)]
    double BrzyckiResult,

    [property: JsonProperty(Required = Required.Always)]
    double EpleyResult,

    [property: JsonProperty(Required = Required.Always)]
    double MayhewResult,

    [property: JsonProperty(Required = Required.Always)]
    double AdamsResult,

    [property: JsonProperty(Required = Required.Always)]
    double BaechleResult,

    [property: JsonProperty(Required = Required.Always)]
    double BergerResult,

    [property: JsonProperty(Required = Required.Always)]
    double BrownResult,

    [property: JsonProperty(Required = Required.Always)]
    double OneRepetitionMaximum,

    [property: JsonProperty(Required = Required.Always)]
    bool isAutomat,

    [property: JsonProperty(Required = Required.Always)]
    LocalDateTime CreatedAt,

    [property: JsonProperty(Required = Required.Always)]
    UserDetailsDto CreatedBy,

    [property: JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    LocalDateTime? ModifiedAt,
    
    [property: JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    UserDetailsDto? ModifiedBy
);
