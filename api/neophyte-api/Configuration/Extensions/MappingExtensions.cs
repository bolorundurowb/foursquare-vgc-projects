using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace neophyte.api.Configuration.Extensions;

internal static class MappingExtensions
{
    public static void ConfigureMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        MapsterConfigExtensions.ConfigureMappings(config);
    }
}