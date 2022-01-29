using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Enums;
using api.Data.ValueObjects;

namespace api.Data.Repositories.Interfaces;

public interface IEventRepository
{
    Task<List<Event>> GetAll(int skip, int limit);

    Task<Event> FindById(string eventId);

    Task<Event> FindByNameAndDate(string name, DateTime date);

    Task<Event> Create(string name, DateTime date, List<(int Priority, Venue Venue)> venuePriority);

    Task<EventSeat> AssignSeat(Event @event, SeatCategory category, Person person);
}