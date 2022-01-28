using logly.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace api.Configuration.Extensions;

internal static class ExceptionExtensions
{
    public static void UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment()) app.UseDeveloperExceptionPage();

        if (!environment.IsProduction())
            app.UseLogly(opts => opts
                .AddRequestMethod()
                .AddStatusCode()
                .AddResponseTime()
                .AddUrl()
                .AddResponseLength());
    }
}