using Mapster;
using neophyte.api.Data.DTOs;
using neophyte.api.Data.Entities;
using neophyte.api.Data.ValueObjects;
using neophyte.api.Models.View;
using neophyte.api.Shared.Media.Implementations;

namespace neophyte.api.Configuration;

public static class MapsterConfigExtensions
{
    public static void ConfigureMappings(TypeAdapterConfig config)
    {
        config.NewConfig<Seat, SeatViewModel>()
            .Map(x => x.Category, y => y.Category.ToString());

        config.NewConfig<Admin, AdminViewModel>()
            .Map(x => x.Id, y => y.Id.ToString());

        config.NewConfig<Newcomer, NewcomerViewModel>()
            .Map(x => x.Id, y => y.Id.ToString());

        config.NewConfig<Attendee, AttendeeViewModel>()
            .Map(x => x.Id, y => y.Id.ToString())
            .Map(x => x.SeatAssigned, y => y.SeatNumber.HasValue ? y.SeatNumber.Value.ToString() : y.SeatAssigned);

        config.NewConfig<DateSummaryDto, DateSummaryViewModel>()
            .AfterMapping((x, y) => { y.HumanReadableDate = y.Date.ToString("ddd, dd MMM yyyy"); });

        config.NewConfig<Person, BasePersonViewModel>()
            .Map(x => x.Id, y => y.Id.ToString());

        config.NewConfig<Person, PersonViewModel>()
            .Inherits<Person, BasePersonViewModel>()
            .Ignore(x => x.QrUrl)
            .Map(x => x.FullName, y => $"{y.FirstName} {y.LastName}".Trim());

        config.NewConfig<Venue, BaseVenueViewModel>()
            .Map(x => x.Id, y => y.Id.ToString())
            .Map(x => x.NumOfSeats, y => y.Seats.Count);

        config.NewConfig<Venue, VenueViewModel>()
            .Inherits<Venue, BaseVenueViewModel>();

        config.NewConfig<EventSeat, EventSeatViewModel>();

        config.NewConfig<Event, BaseEventViewModel>()
            .Map(x => x.Id, y => y.Id.ToString())
            .Map(x => x.NumOfAttendees, y => y.AssignedSeats.Count);

        config.NewConfig<Event, EventViewModel>()
            .Inherits<Event, BaseEventViewModel>()
            .AfterMapping((model, vm) =>
            {
                vm.RegistrationUrlQrCode = QrCodeService.GenerateQrCode(model.RegistrationUrl);
            });

        config.NewConfig<EventAttendeeDto, EventAttendeeViewModel>();
    }
}