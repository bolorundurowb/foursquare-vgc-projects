using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace api.Configuration.Extensions
{
    internal static class ApiExtensions
    {
        public static void ConfigureApi(this IServiceCollection services)
        {
            services.AddCors();
            services.AddRouting(option => option.LowercaseUrls = true);
            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddApiVersioning(options => { options.ReportApiVersions = true; });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static void UseApi(this IApplicationBuilder app)
        {
            app.UseCors(options => options
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuth();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}