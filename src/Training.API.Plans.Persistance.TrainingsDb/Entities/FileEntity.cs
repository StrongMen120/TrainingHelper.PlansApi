using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training.API.Plans.Persistance.TrainingsDb.Entities;

[Table("Files", Schema = TrainingDatabaseConstants.DefaultSchema)]
public class FileEntity
{
    [Key]
    public Guid Identifier { get; set; }
    public string PhotoId { get; set; }
    public long ExerciseInfoIdentifier { get; set; }
    public virtual ExerciseInfoEntity ExerciseInfo { get; set; }
}
