
using Training.API.Plans.Core.Domain;

namespace Training.API.Plans.Core.Abstraction.Services;

public interface IUserCache
{
    Task<UserInfoModel> CheckUserId(long userId);
    Task<int> InvalidateCacheAsync();
}