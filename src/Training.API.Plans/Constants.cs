namespace Training.API.Plans;

internal static class Constants
{
    internal const string AppName = "training-api-plans";
    internal static class ConfigSections
    {
        internal const string Serilog = "Serilog";

        internal static class Swagger
        {
            internal const string SectionName = "Swagger";
            internal const string SecurityDefinition = $"{SectionName}:SecurityDefinition:OAuth2";
        }

        internal static class Authentication
        {
            internal const string SectionName = "Authentication";
            internal const string DefaultConfig = $"{SectionName}:DefaultConfig";
            internal const string JwtBearer = $"{SectionName}:JWTBearer";
        }
    }
}
