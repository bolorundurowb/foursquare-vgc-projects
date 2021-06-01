using System.Text.Json.Serialization;
using logly.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddApiVersioning();
        }
        
        public static void UseApi(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            if (!environment.IsProduction())
            {
                app.UseLogly(opts => opts
                    .AddRequestMethod()
                    .AddStatusCode()
                    .AddResponseTime()
                    .AddUrl()
                    .AddResponseLength());
            }

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