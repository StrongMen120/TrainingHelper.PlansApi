using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Training.API.Plans.Persistance.TrainingsDb.Entities;

[Table("PlannedExercisesEntity", Schema = TrainingDatabaseConstants.DefaultSchema)]
public class PlannedExercisesEntity
{
    [Key]
    public Guid Identifier { get; set; }

    [Required]
    public long PlansId { get; set; }

    [Required]
    public long ExerciseInfoId { get; set; }

    [Required]
    public int Series { get; set; }

    [Required]
    public string Reps { get; set; }

    [Required]
    public string Weight { get; set; }

    [Required]
    public int Rate { get; set; }

    [Required]
    public int RPE { get; set; }

    [Required]
    public int BrakeSeconds { get; set; }

    
    public LocalDateTime? ModifiedAt { get; set; }

    
    public UserDetails? ModifiedBy { get; set; }

    
    public LocalDateTime CreatedAt { get; set; }

    
    public UserDetails CreatedBy { get; set; }
    public virtual ExerciseInfoEntity ExerciseInfo { get; set; } = null!;
    public virtual PlansEntity Plans { get; set; } = null!;
}