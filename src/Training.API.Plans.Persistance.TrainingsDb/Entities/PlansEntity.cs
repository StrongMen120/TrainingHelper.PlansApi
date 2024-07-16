using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Training.API.Plans.Persistance.TrainingsDb.Entities;

[Table("PlansEntity", Schema = TrainingDatabaseConstants.DefaultSchema)]
public class PlansEntity
{
    [Key]
    public long Identifier { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }
    
    [Required]
    public long AuthorId { get; set; }
    
    [Required]
    public PlansImage Image { get; set; }

    public LocalDateTime? ModifiedAt { get; set; }
    
    public UserDetails? ModifiedBy { get; set; }
    
    public LocalDateTime CreatedAt { get; set; }
    
    public UserDetails CreatedBy { get; set; }
    public virtual ICollection<PlannedTrainingsEntity> PlannedTrainings { get; set; } = new List<PlannedTrainingsEntity>();
    public virtual ICollection<PlannedExercisesEntity> PlannedExercises { get; set; } = new List<PlannedExercisesEntity>();
}