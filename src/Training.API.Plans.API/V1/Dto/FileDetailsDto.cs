using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Training.API.Plans.API.V1.DTOs;

[ApiVersion("1")]
public record FileDetailsDto(
    [property: JsonProperty(Required = Required.Always)]
    Guid Identifier,
    
    [property: JsonProperty(Required = Required.Always)]
    string Name,

    [property: JsonProperty(Required = Required.Always)]
    long Size,

    [property: JsonProperty(Required = Required.Always)]
    string Type,

    [property: JsonProperty(Required = Required.Always)]
    byte[] Bytes
);