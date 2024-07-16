using System.Collections.Concurrent;
using System.Net;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Integrations.Mappings;
using Training.API.Users.Integration.Api;
using Training.API.Users.Integration.Client;
using Training.Common.Hexagon.Core.Exceptions;
using Training.Common.Hexagon.Integration;

namespace Training.API.Plans.Integrations.Services;

public class UserCache : IUserCache
{
    private readonly IUsersApi usersApi;

    public UserCache(IUsersApi usersApi)
    {
        this.usersApi = usersApi;
    }
    private ConcurrentDictionary<long, UserInfoModel> UsersInfo { get; } = new();

    public async Task<UserInfoModel> CheckUserId(long userId)
    {
        try
        {
            var isCacheHit = this.UsersInfo.TryGetValue(userId, out var userInfoModel);
            if (!isCacheHit)
            {
                var userDto = await this.usersApi.GetUserAsync(userId);
                userInfoModel = new UserInfoModel(userId, $"{userDto.FirstName} {userDto.SecondName}", userDto.GroupsId, userDto.RolesId.Where(e => e == 2).Any());
                this.UsersInfo.AddOrUpdate(userId, (_) => userInfoModel, (_, _) => userInfoModel);
            }

            return userInfoModel;
        }
        catch (ApiException e) when (e.ErrorCode == (int)HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"User with id '{userId}' not found!", e);
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }

    public async Task<int> InvalidateCacheAsync()
    {
        var cachedEntries = this.UsersInfo.Count;

        this.UsersInfo.Clear();

        await ValueTask.CompletedTask;

        return cachedEntries;
    }
}