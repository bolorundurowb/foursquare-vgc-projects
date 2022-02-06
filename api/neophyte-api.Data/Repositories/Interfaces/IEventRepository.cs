using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using neophyte.api.Data.DTOs;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Enums;
using neophyte.api.Data.ValueObjects;

namespace neophyte.api.Data.Repositories.Interfaces;

public interface IEventRepository
{
    Task<List<Event>> GetAll(int skip, int limit);

    Task<Event> FindById(string eventId);

    Task<Event> FindByNameAndDate(string name, DateTime date);

    Task<Event> Create(string name, DateTime date, List<(int Priority, Venue Venue)> venuePriority);

    Task<EventSeat> AssignSeat(Event @event, SeatCategory category, Person person);

    Task<EventSeat> ChangeSeat(Event @event, Person person, string seatNumber);

    Task<List<EventAttendeeDto>> GetAttendees(Event @event);

    Task<(string, byte[])> GetAttendeeReport(Event @event);
}