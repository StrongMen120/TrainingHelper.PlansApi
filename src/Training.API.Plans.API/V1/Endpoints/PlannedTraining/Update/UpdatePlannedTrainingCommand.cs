using Newtonsoft.Json;
using NodaTime;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.API.V1.Endpoints.PlannedTraining.Update;

public sealed record UpdatePlannedTrainingCommand
(
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
    long? GroupId
);
