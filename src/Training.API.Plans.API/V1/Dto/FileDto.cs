using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Training.API.Plans.API.V1.DTOs;

[ApiVersion("1")]
public record FileDto(
    [property: JsonProperty(Required = Required.Always)]
    Guid Identifier,
    
    [property: JsonProperty(Required = Required.Always)]
    string PhotoId
);