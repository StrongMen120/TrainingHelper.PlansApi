using NodaTime;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.Core.Domain;

public record PlansModel(
    long Identifier,
    string Name,
    string Description,
    PlansImage Image,
    long AuthorId,
    IEnumerable<PlannedExercisesModel> PlannedExercise,
    LocalDateTime? ModifiedAt,
    UserDetailsModel? ModifiedBy,
    LocalDateTime CreatedAt,
    UserDetailsModel CreatedBy
);