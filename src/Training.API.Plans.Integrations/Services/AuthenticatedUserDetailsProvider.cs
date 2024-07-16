using System.Security.Claims;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;

namespace Training.API.Plans.Integrations.Services;

public class AuthenticationDetailsProvider : IAuthenticationDetailsProvider
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly IAuthenticationApiClient authenticationApi;
    private readonly Lazy<ValueTask<string?>> tokenCache;
    private readonly Lazy<ValueTask<UserInfo?>> userInfoCache;

    public AuthenticationDetailsProvider(IHttpContextAccessor contextAccessor, IAuthenticationApiClient authenticationApi)
    {
        this.contextAccessor = contextAccessor;
        this.authenticationApi = authenticationApi;

        tokenCache = new Lazy<ValueTask<string?>>(async () => await (contextAccessor.HttpContext?.GetTokenAsync("access_token") ?? Task.FromResult<string?>(null)));
        userInfoCache = new Lazy<ValueTask<UserInfo?>>(async () => await this.authenticationApi.GetUserInfoAsync(await tokenCache.Value));
    }

    public async Task<UserInfo?> GetCurrentUserInfoAsync() => await this.userInfoCache.Value;

    private Claim? GetCurrentUserClaim(Func<Claim, bool> predicate) => contextAccessor.HttpContext?.User.Claims.FirstOrDefault(predicate);

    public async Task<string?> GetCurrentUserId() => this.GetCurrentUserClaim(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? (await GetCurrentUserInfoAsync())?.UserId;
    public async Task<string?> GetCurrentUserFullName() => this.GetCurrentUserClaim(c => c.Type == ClaimTypes.Name)?.Value ?? (await GetCurrentUserInfoAsync())?.FullName;

    public async Task<UserDetailsModel> GetUserDetails() => new UserDetailsModel(await GetCurrentUserId(), await GetCurrentUserFullName());

}
