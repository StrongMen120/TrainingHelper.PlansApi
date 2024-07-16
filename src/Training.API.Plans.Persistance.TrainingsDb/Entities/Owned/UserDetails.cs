using Microsoft.EntityFrameworkCore;

namespace Training.API.Plans.Persistance.TrainingsDb.Entities;

[Owned]
public record UserDetails
{
    public string Id { get; set; }

    public string FullName { get; set; }
}
