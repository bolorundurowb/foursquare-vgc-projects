using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Repositories.Interfaces;
using meerkat;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Implementations;

public class EventRepository : IEventRepository
{
    public Task<List<Event>> GetAll(int skip, int limit) =>
        Meerkat.Query<Event>()
            .OrderByDescending(x => x.Date)
            .Skip(skip)
            .Take(limit)
            .ToListAsync();

    public Task<Event> FindByNameAndDate(DateTime date, string name) =>
        Meerkat.FindOneAsync<Event>(x => x.Name == name && x.Date == date);

    public async Task<Event> Create(string name, DateTime date, List<(int Priority, Venue Venue)> venuePriority)
    {
        var _event = new Event(name, date, venuePriority);
        await _event.SaveAsync();

        return _event;
    }
}