using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace Training.API.Plans.API.V1.DTOs;

[ApiVersion("1")]
public record AddTestEmployersDto(
    [property: JsonProperty(Required = Required.Always)]
    string FirstName,

    [property: JsonProperty(Required = Required.Always)]
    string LastName,

    [property: JsonProperty(Required = Required.Always)]
    string Email,

    [property: JsonProperty(Required = Required.Always)]
    string PhoneNumber,

    [property: JsonProperty(Required = Required.Always)]
    DateTime DateOfBirth,

    [property: JsonProperty(Required = Required.Always)]
    string Department,

    [property: JsonProperty(Required = Required.Always)]
    string JobTitle,

    [property: JsonProperty(Required = Required.Always)]
    string Salary,

    [property: JsonProperty(Required = Required.Always)]
    DateTime HireDate,

    [property: JsonProperty(Required = Required.Always)]
    string Address,

    [property: JsonProperty(Required = Required.Always)]
    string Password
);
