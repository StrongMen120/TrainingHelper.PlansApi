using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Training.API.Plans.Persistance.TrainingsDb.Entities;

[Table("ExerciseRecords", Schema = TrainingDatabaseConstants.DefaultSchema)]
public class ExerciseRecordsEntity
{
    [Key]
    public long Identifier { get; set; }

    [Required]
    public long Revision { get; set; }

    [Required]
    public LocalDate Date { get; set; }

    [Required]
    public int Reps { get; set; }

    [Required]
    public double Weight { get; set; }

    [Required]
    public double LombardiResult { get; set; }

    [Required]
    public double BrzyckiResult { get; set; }

    [Required]
    public double EpleyResult { get; set; }

    [Required]
    public double MayhewResult { get; set; }

    [Required]
    public double AdamsResult { get; set; }

    [Required]
    public double BaechleResult { get; set; }

    [Required]
    public double BergerResult { get; set; }

    [Required]
    public double BrownResult { get; set; }

    [Required]
    public double OneRepetitionMaximum { get; set; }
    
    [Required]
    public bool isAutomat { get; set; }

    public LocalDateTime CreatedAt { get; set; }

    public UserDetails CreatedBy { get; set; }
    public virtual ExerciseRecordsRegistryEntity RegistryEntry { get; set; }
}