using System.Security.Policy;

namespace Training.API.Plans;

internal static class Constants
{
    internal static class ExternalServiceSection
    {
        internal const string Root = "ExternalService";
        internal const string UsersApi = $"{Root}:users-api";
        internal const string AuthenticationApi = $"{Root}:AuthenticationApi";
        internal const string S3Storage = $"{Root}:S3Storage";
    }
}
