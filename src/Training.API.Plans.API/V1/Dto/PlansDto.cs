using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NodaTime;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.API.V1.DTOs;

[ApiVersion("1")]
public record PlansDto(
    [property: JsonProperty(Required = Required.Always)]
    long Identifier,
    
    [property: JsonProperty(Required = Required.Always)]
    string Name,
    
    [property: JsonProperty(Required = Required.Always)]
    string Description,

    [property: JsonProperty(Required = Required.Always)]
    PlansImage Image,
    
    [property: JsonProperty(Required = Required.Always)]
    long AuthorId,
    
    [property: JsonProperty(Required = Required.Always)]
    IEnumerable<PlannedExercisesDto> PlannedExercise,

    [property: JsonProperty(Required = Required.Always)]
    LocalDateTime CreatedAt,

    [property: JsonProperty(Required = Required.Always)]
    UserDetailsDto CreatedBy,

    [property: JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    LocalDateTime? ModifiedAt,
    
    [property: JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    UserDetailsDto? ModifiedBy
);
