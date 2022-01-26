using api.Data.Repositories.Implementations;
using api.Data.Repositories.Interfaces;
using api.Shared.Email.Implementations;
using api.Shared.Email.Interfaces;
using api.Shared.Media.Implementations;
using api.Shared.Media.Interfaces;
using meerkat;
using Microsoft.Extensions.DependencyInjection;

namespace api.Configuration.Extensions;

internal static class InjectionExtensions
{
    public static void ConfigureInjection(this IServiceCollection services)
    {
        Meerkat.Connect(Config.DbUrl);
        services.AddHttpClient();
        services.AddScoped<IEmailService, MailgunService>();
        services.AddScoped<IQrCodeService, QrCodeService>();
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<INewcomersRepository, NewcomersRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IPersonsRepository, PersonsRepository>();
        services.AddScoped<IVenueRepository, VenueRepository>();
    }
}