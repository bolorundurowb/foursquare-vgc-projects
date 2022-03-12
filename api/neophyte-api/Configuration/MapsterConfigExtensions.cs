using System;
using System.Linq;
using Mapster;
using neophyte.api.Data.DTOs;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Enums;
using neophyte.api.Data.ValueObjects;
using neophyte.api.Models.View;
using neophyte.api.Shared.Media.Implementations;

namespace neophyte.api.Configuration;

public static class MapsterConfigExtensions
{
    public static void ConfigureMappings(TypeAdapterConfig config)
    {
        config.NewConfig<Seat, SeatViewModel>();

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

        config.NewConfig<EventSeat, EventSeatViewModel>()
            .Map(x => x.VenueId, y => y.VenueId.ToString());

        config.NewConfig<Event, BaseEventViewModel>()
            .Map(x => x.Id, y => y.Id.ToString())
            .AfterMapping((model, vm) =>
            {
                var sum = model.OnlineAttendance.Count();
                sum += model.AssignedSeats
                    .Count(x => x.Category == SeatCategory.Single);
                sum += model.AssignedSeats
                    .Count(x => x.Category == SeatCategory.Couples);

                vm.NumOfAttendees = sum;
            });

        config.NewConfig<Event, EventViewModel>()
            .Inherits<Event, BaseEventViewModel>()
            .Map(x => x.DurationInMinutes, y => Convert.ToInt32((y.EndsAt - y.StartsAt).TotalMinutes))
            .Map(x => x.IsRegistrationClosed, y => y.IsRegistrationClosed())
            .AfterMapping((model, vm) =>
            {
                vm.RegistrationUrlQrCode = QrCodeService.GenerateQrCode(model.RegistrationUrl);

                // map seats
                vm.AvailableSeats = model.AvailableSeats
                    .GroupBy(x => x.VenueId)
                    .Select(x =>
                    {
                        var venueId = x.Key;
                        var venueName = x.First().VenueName;
                        var seats = x.Select(y => new SeatViewModel
                        {
                            Category = y.Category,
                            AssociatedNumber = y.AssociatedNumber,
                            Number = y.Number
                        }).ToList();

                        return new VenueViewModel
                        {
                            Id = venueId.ToString(),
                            Name = venueName,
                            NumOfSeats = seats.Count,
                            Seats = seats
                        };
                    })
                    .ToList();
            });

        config.NewConfig<EventAttendeeDto, EventAttendeeViewModel>();
    }
}