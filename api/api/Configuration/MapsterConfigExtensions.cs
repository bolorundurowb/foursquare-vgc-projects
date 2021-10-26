using api.Data.DTOs;
using api.Data.Models;
using api.Models.View;
using Mapster;

namespace api.Configuration
{
    public static class MapsterConfigExtensions
    {
        public static void ConfigureMappings(TypeAdapterConfig config)
        {
            config.NewConfig<Admin, AdminViewModel>()
                .Map(x => x.Id, y => y.Id.ToString());

            config.NewConfig<Newcomer, NewcomerViewModel>()
                .Map(x => x.Id, y => y.Id.ToString());

            config.NewConfig<Attendee, AttendeeViewModel>()
                .Map(x => x.Id, y => y.Id.ToString())
                .Map(x => x.SeatAssigned, y => y.SeatNumber.HasValue ? y.SeatNumber.Value.ToString() : y.SeatAssigned);

            config.NewConfig<DateSummaryDto, DateSummaryViewModel>()
                .AfterMapping((x, y) => { y.HumanReadableDate = y.Date.ToString("ddd, dd MMM yyyy"); });

            config.NewConfig<Person, PersonViewModel>()
                .Map(x => x.FullName, y => $"{y.FirstName} {y.LastName}".Trim());
        }
    }
}