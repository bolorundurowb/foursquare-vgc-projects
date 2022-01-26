using api.Configuration.Extensions.SwaggerFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace api.Configuration.Extensions;

internal static class DocumentationExtensions
{
    public static void ConfigureDocs(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options => { options.OperationFilter<SwaggerDefaultValues>(); });
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