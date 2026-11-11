using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Notes.WebApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach(var description in _provider.ApiVersionDescriptions)
            {
                var apiVersion = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = $"Notes API {apiVersion}",
                        Description = "A Simple example ASP NET Core Web API.",
                        TermsOfService =
                            new Uri("https://t.me/cccooop54"),
                        Contact = new OpenApiContact
                        {
                            Name = "NonblaYY",
                            Email = string.Empty,
                            Url = new Uri("https://t.me/cccooop54")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "OAO MegaLul",
                            Url = new Uri("https://t.me/cccooop54")
                        }
                    });

                options.AddSecurityDefinition($"AuthToken{apiVersion}",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Name = "Authorization",
                        Description = "Authorization token"
                    });

                options.AddSecurityRequirement(document =>
                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference($"AuthToken{apiVersion}", document)] = new List<string>()
                });

                options.CustomOperationIds(apiDescription =>
                    apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
                        ? methodInfo.Name
                        : null);
            }
        }
    }
}