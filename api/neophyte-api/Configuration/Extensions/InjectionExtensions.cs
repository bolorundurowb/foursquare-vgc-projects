using api.Data.Repositories.Implementations;
using api.Data.Repositories.Interfaces;
using api.Shared.Email.Implementations;
using api.Shared.Email.Interfaces;
using meerkat;
using Microsoft.Extensions.DependencyInjection;

namespace neophyte;

internal static class InjectionExtensions
{
    public static void ConfigureInjection(this IServiceCollection services)
    {
        Meerkat.Connect(Config.DbUrl);
        services.AddHttpClient();
        services.AddScoped<IEmailService, MailgunService>();
        services.AddScoped<IVenueRepository, VenueRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IPersonsRepository, PersonsRepository>();
        services.AddScoped<INewcomersRepository, NewcomersRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
    }
}