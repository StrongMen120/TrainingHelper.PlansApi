using NodaTime;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.Core.Domain;

public record TestEmployersModel(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    string Department,
    string JobTitle,
    string Salary,
    DateTime HireDate,
    string Address,
    string Password
);