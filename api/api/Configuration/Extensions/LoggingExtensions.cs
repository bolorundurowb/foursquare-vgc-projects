using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rollbar.NetCore.AspNet;

namespace api.Configuration.Extensions
{
    internal static class LoggingExtensions
    {
        public static void ConfigureLogging(this IServiceCollection services)
        {
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