using api.Data;
using api.Data.Repositories.Implementations;
using api.Data.Repositories.Interfaces;
using api.Shared.Email.Implementations;
using api.Shared.Email.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace api.Configuration.Extensions
{
    internal static class InjectionExtensions
    {
        public static void ConfigureInjection(this IServiceCollection services)
        {
            services.AddSingleton(new DbContext(Config.DbServerUrl, Config.DbName));
            services.AddHttpClient();
            services.AddScoped<IEmailService, MailgunService>();
            services.AddScoped<IAdminsRepository, AdminsRepository>();
            services.AddScoped<INewcomersRepository, NewcomersRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IPersonsRepository, PersonsRepository>();
        }
    }
}