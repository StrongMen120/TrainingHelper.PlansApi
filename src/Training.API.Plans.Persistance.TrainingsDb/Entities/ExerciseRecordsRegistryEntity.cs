using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Training.API.Plans.Persistance.TrainingsDb.Entities;

[Table("ExerciseRecordsRegistry", Schema = TrainingDatabaseConstants.DefaultSchema)]
public class ExerciseRecordsRegistryEntity
{
    [Required]
    public long Identifier { get; set; }

    [Required]
    public long LatestRevision { get; set; }

    [Required]
    public long ExerciseId { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    public ExerciseInfoEntity ExerciseInfo { get; set; }

    public LocalDateTime CreatedAt { get; set; }

    public UserDetails CreatedBy { get; set; }
    public virtual ICollection<ExerciseRecordsEntity> ExerciseRecords { get; set; } = new HashSet<ExerciseRecordsEntity>();
}