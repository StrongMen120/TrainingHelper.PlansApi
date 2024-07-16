using NodaTime;
using Training.API.Plans.Core.Domain.Values;

namespace Training.API.Plans.Core.Domain;

public record ExercisesInfoDetailsModel(
    long Identifier,
    string Name,
    string Description,
    long AuthorId,
    IEnumerable<BodyElements> BodyElements,
    IEnumerable<FileDetailsModel> Files,
    LocalDateTime CreatedAt,
    UserDetailsModel CreatedBy,
    LocalDateTime? ModifiedAt,
    UserDetailsModel? ModifiedBy
);