using api.Data.Models;
using api.Models.View;
using Mapster;

namespace api.Configuration
{
    public static class MapsterConfigExtensions
    {
        public static void ConfigureMappings(TypeAdapterConfig config)
        {
            config
                .NewConfig<Admin, AdminViewModel>()
                .Map(x => x.Id, y => y.Id.ToString());

            config
                .NewConfig<Newcomer, NewcomerViewModel>()
                .Map(x => x.Id, y => y.Id.ToString());

            config
                .NewConfig<Attendee, AttendeeViewModel>()
                .Map(x => x.Id, y => y.Id.ToString());
        }
    }
}
