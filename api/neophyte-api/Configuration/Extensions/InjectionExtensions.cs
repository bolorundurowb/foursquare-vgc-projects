using meerkat;
using Microsoft.Extensions.DependencyInjection;
using neophyte.api.Data.Repositories.Implementations;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Shared.Email.Implementations;
using neophyte.api.Shared.Email.Interfaces;

namespace neophyte.api.Configuration.Extensions;

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