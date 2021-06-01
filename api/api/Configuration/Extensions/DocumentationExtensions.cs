using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace api.Configuration.Extensions
{
    internal static class DocumentationExtensions
    {
        public static void ConfigureDocs(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Neophyte API",
                    Version = "v1"
                });
            });
        }

        public static void UseDocs(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Neophyte API");
                x.RoutePrefix = "docs";
            });
        }
    }
}