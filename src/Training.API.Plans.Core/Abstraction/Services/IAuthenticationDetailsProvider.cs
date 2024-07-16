using Training.API.Plans.Core.Domain;

namespace Training.API.Plans.Core.Abstraction.Services;
public interface IAuthenticationDetailsProvider
{
    Task<string?> GetCurrentUserId();
    Task<UserDetailsModel> GetUserDetails();
}