using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Training.API.Plans.Persistance.TrainingsDb.Entities;

[Table("TestEmployers", Schema = TrainingDatabaseConstants.DefaultSchema)]
public class TestEmployerEntity
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string Department { get; set; }

    [Required]
    public string JobTitle { get; set; } 

    [Required]
    public string Salary { get; set; }

    [Required]
    public DateTime HireDate { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Password { get; set; }
}