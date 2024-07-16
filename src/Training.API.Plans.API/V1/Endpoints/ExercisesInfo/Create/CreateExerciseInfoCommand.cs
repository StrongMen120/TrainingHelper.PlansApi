using Newtonsoft.Json;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.Create;

public sealed record CreateExerciseInfoCommand
(
    [property: JsonProperty(Required = Required.Always)]
    string Name,

    [property: JsonProperty(Required = Required.Always)]
    string Description,

    [property: JsonProperty(Required = Required.Always)]
    long AuthorId,
    
    [property: JsonProperty(Required = Required.Default)]
    IEnumerable<BodyElements> BodyElements
);
