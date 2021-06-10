using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Rollbar.NetCore.AspNet;

namespace api.Configuration.Extensions
{
    internal static class LoggingExtensions
    {
        public static void ConfigureLogging(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRollbarLogger(loggerOptions =>
            {
                loggerOptions.Filter =
                    (loggerName, loglevel) => loglevel >= LogLevel.Trace;
            });
        }

        public static void UseLogging(this IApplicationBuilder app)
        {
            app.UseRollbarMiddleware();
        }
    }
}