namespace Training.API.Plans.Core.Domain;

public record UserInfoModel(
    long Id,
    string FullName,
    IEnumerable<long> Groups,
    bool IsTrainer);
