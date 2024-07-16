using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Training.API.Plans.Persistance.TrainingsDb.Entities;

[Table("PlannedTrainingsEntity", Schema = TrainingDatabaseConstants.DefaultSchema)]
public class PlannedTrainingsEntity
{
    [Key]
    public Guid Identifier { get; set; }

    [Required]
    public long PlansId { get; set; }
    
    [Required]
    public PlansType PlansType { get; set; }

    [Required]
    public LocalDateTime DateStart { get; set; }

    [Required]
    public LocalDateTime DateEnd { get; set; }
    public long? UserId { get; set; }
    public long? TrainerId { get; set; }

    public long? GroupId { get; set; }

    public LocalDateTime? ModifiedAt { get; set; }

    
    public UserDetails? ModifiedBy { get; set; }

    
    public LocalDateTime CreatedAt { get; set; }

    
    public UserDetails CreatedBy { get; set; }
    public virtual PlansEntity Plans { get; set; } = null!;
}