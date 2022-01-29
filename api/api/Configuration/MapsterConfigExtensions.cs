using api.Data.DTOs;
using api.Data.Entities;
using api.Data.ValueObjects;
using api.Models.View;
using api.Shared.Media.Implementations;
using Mapster;

namespace api.Configuration;

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

        config.NewConfig<Person, PersonViewModel>()
            .Ignore(x => x.QrUrl)
            .Map(x => x.FullName, y => $"{y.FirstName} {y.LastName}".Trim());

        config.NewConfig<Venue, BaseVenueViewModel>()
            .Map(x => x.Id, y => y.Id.ToString())
            .Map(x => x.NumOfSeats, y => y.Seats.Count);

        config.NewConfig<Venue, VenueViewModel>()
            .Inherits<Venue, BaseVenueViewModel>();

        config.NewConfig<EventSeat, EventSeatViewModel>()
            .Map(x => x.Category, y => y.Category.ToString());

        config.NewConfig<Event, BaseEventViewModel>()
            .Map(x => x.Id, y => y.Id.ToString())
            .Map(x => x.NumOfAttendees, y => y.AssignedSeats.Count);

        config.NewConfig<Event, EventViewModel>()
            .Inherits<Event, BaseEventViewModel>()
            .AfterMapping((model, vm) =>
            {
                vm.RegistrationUrlQrCode = QrCodeService.GenerateQrCode(model.RegistrationUrl);
            });
    }
}