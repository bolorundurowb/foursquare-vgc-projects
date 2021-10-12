using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace api.Configuration.Extensions
{
    internal static class LoggingExtensions
    {
        public static void ConfigureLogging(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void UseLogging(this IApplicationBuilder app)
        {
        }
    }
}
