using dotenv.net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using neophyte.api.Configuration.Extensions;
using neophyte.api.Data.Extensions;

namespace neophyte.api;

public class Startup
{
    private readonly IWebHostEnvironment _environment;

    public Startup(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        if (_environment.IsDevelopment())
            DotEnv.Fluent()
                .WithDefaultEncoding()
                .WithProbeForEnv()
                .WithExceptions()
                .Load();

        services.ConfigureLogging();
        services.ConfigureApi();
        services.ConfigureDocs();
        services.ConfigureAuth();
        services.ConfigureMapping();
        services.ConfigureInjection();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseExceptionHandling(_environment);
        app.UseLogging();
        app.UseApi();
        app.UseDocs(provider);
            
        DbExtensions.SeedDefaults();
    }
}