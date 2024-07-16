namespace Training.API.Plans.API;

internal static class Constants
{
    internal const string AppName = "training-api-plans";
    internal const string ApiTitle = "Training Plans Api";

    internal static class ConfigSections
    {

        internal static class Swagger
        {
            internal const string SectionName = "Swagger";
            internal const string SecurityDefinition = $"{SectionName}:SecurityDefinition:OAuth2";
        }
    }
}
