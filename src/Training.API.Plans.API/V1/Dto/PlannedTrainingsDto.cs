using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NodaTime;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.API.V1.DTOs;

[ApiVersion("1")]
public record PlannedTrainingsDto(
    [property: JsonProperty(Required = Required.Always)]
    Guid Identifier,
    
    [property: JsonProperty(Required = Required.Always)]
    long PlansId,
    
    [property: JsonProperty(Required = Required.Always)]
    PlansType PlansType,
    
    [property: JsonProperty(Required = Required.Always)]
    LocalDateTime DateStart,
    
    [property: JsonProperty(Required = Required.Always)]
    LocalDateTime DateEnd,
    
    [property: JsonProperty(Required = Required.Default)]
    long? UserId,
    
    [property: JsonProperty(Required = Required.Default)]
    long? TrainerId,
    
    [property: JsonProperty(Required = Required.Default)]
    long? GroupId,
    
    [property: JsonProperty(Required = Required.Always)]
    PlansDto Plans,

    [property: JsonProperty(Required = Required.Always)]
    LocalDateTime CreatedAt,

    [property: JsonProperty(Required = Required.Always)]
    UserDetailsDto CreatedBy,

    [property: JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    LocalDateTime? ModifiedAt,
    
    [property: JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    UserDetailsDto? ModifiedBy
);
