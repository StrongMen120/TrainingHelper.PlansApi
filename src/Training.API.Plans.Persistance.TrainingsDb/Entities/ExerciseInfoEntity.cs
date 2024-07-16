using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Training.API.Plans.Persistance.TrainingsDb.Entities;

[Table("ExerciseInfo", Schema = TrainingDatabaseConstants.DefaultSchema)]
public class ExerciseInfoEntity
{
    [Key]
    public long Identifier { get; set; }

    [Required]
    public string BodyElements { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public long AuthorId { get; set; }
    
    public LocalDateTime? ModifiedAt { get; set; }

    public UserDetails? ModifiedBy { get; set; }

    public LocalDateTime CreatedAt { get; set; }

    public UserDetails CreatedBy { get; set; }
    public virtual ICollection<ExerciseRecordsRegistryEntity> CurrentRecords { get; set; }
    public virtual ICollection<PlannedExercisesEntity> PlannedExercises { get; set; } = new List<PlannedExercisesEntity>();
    public virtual ICollection<DoneExercisesEntity> DoneExercises { get; set; } = new List<DoneExercisesEntity>();
    public virtual ICollection<FileEntity>? Files { get; set; } = new List<FileEntity>();
}