using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using neophyte.api.Configuration.Extensions.SwaggerFilters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace neophyte.api.Configuration.Extensions;

internal static class DocumentationExtensions
{
    public static void ConfigureDocs(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.OperationFilter<SwaggerDefaultValues>();

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var xmlPath = Path.Combine(baseDirectory, "neophyte-api.xml");
            
            options.IncludeXmlComments(xmlPath);
        });
    }

    public static void UseDocs(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger(options => { options.RouteTemplate = "docs/{documentName}/docs.json"; });
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "docs";
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/docs/{description.GroupName}/docs.json",
                    description.GroupName.ToUpperInvariant());
        });
    }
}