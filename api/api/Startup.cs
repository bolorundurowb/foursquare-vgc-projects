using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using api.Configuration;
using api.Data;
using api.Data.Extensions;
using api.Data.Repositories.Implementations;
using api.Data.Repositories.Interfaces;
using api.Shared.Email.Implementations;
using api.Shared.Email.Interfaces;
using dotenv.net.DependencyInjection.Microsoft;
using logly.Extensions;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace api
{
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
            // read the environment vars
            services.AddEnv(builder =>
            {
                builder
                    .AddEncoding(Encoding.Default)
                    .AddEnvFile(Path.GetFullPath(".env"))
                    .AddThrowOnError(_environment.IsDevelopment());
            });
            services.AddEnvReader();

            services.AddCors();

            services.AddRouting(option => option.LowercaseUrls = true);

            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Neophyte API",
                    Version = "v1"
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = Config.Audience,
                        ValidIssuer = Config.Issuer,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.Secret))
                    };
                });

            // add in deb context
            services.AddSingleton(new DbContext(Config.DbServerUrl, Config.DbName));

            // add http client factory
            services.AddHttpClient();

            // add in mapper
            var config = TypeAdapterConfig.GlobalSettings;
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            MapsterConfigExtensions.ConfigureMappings(config);

            // add DI mappings
            services.AddScoped<IEmailService, MailgunService>();
            services.AddScoped<IAdminsRepository, AdminsRepository>();
            services.AddScoped<INewcomersRepository, NewcomersRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, DbContext context)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!_environment.IsProduction())
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

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Neophyte API");
                x.RoutePrefix = "docs";
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // seed admins
            context.SeedDefaults();
        }
    }
}