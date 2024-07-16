using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.Update;

public sealed record UpdateExerciseInfoCommand
(
    [property: JsonProperty(Required = Required.Always)]
    long UpdatedId,

    [property: JsonProperty(Required = Required.Always)]
    string Name,

    [property: JsonProperty(Required = Required.Always)]
    string Description,
    
    [property: JsonProperty(Required = Required.Default)]
    IEnumerable<BodyElements> BodyElements
);
