
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NodaTime;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Training.Common.Utils.SwashbuckleHelper;

namespace Training.API.Plans.API.Configuration;

public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        //options.ConfigureForNodaTime();
        options.SchemaGeneratorOptions.CustomTypeMappings[typeof(LocalDate)] = () => new OpenApiSchema
        {
            Type = "string",
            Format = "date",
        };

        options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
        options.UseOneOfForPolymorphism();
        options.UseAllOfForInheritance();

        options.AddSecurityDefinition("BearerJWT", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer scheme.",
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerJWT" },
                    },
                    Array.Empty<string>()
                },
            });

        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc("all", new OpenApiInfo
            {
                Title = $"{Constants.ApiTitle} - All Versions",
                Version = "all",
            });
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        // Configure the way documentation is generated
        options.DocInclusionPredicate(DocInclusionStrategy.Flexible);
        // options.DocumentFilter<AlphabeticalOperationsSortFilter>();
        // options.OperationFilter<AuthorizeResponsesOperationFilter>();
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = $"{Constants.ApiTitle} - v{description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
        };

        if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

        return info;
    }
}