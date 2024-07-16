using NodaTime;

namespace Training.API.Plans.Core.Domain;

public record PlannedTrainingsModel(
    Guid Identifier,
    long PlansId,
    Values.PlansType PlansType,
    LocalDateTime DateStart,
    LocalDateTime DateEnd,
    long? UserId,
    long? TrainerId,
    long? GroupId,
    PlansModel Plans,
    LocalDateTime? ModifiedAt,
    UserDetailsModel? ModifiedBy,
    LocalDateTime CreatedAt,
    UserDetailsModel CreatedBy
);