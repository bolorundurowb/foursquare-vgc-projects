using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Entities;

namespace api.Data.Repositories.Interfaces;

public interface IEventRepository
{
    Task<List<Event>> GetAll(int skip, int limit);

    Task<Event> FindByNameAndDate(DateTime date, string name);

    Task<Event> Create(string name, DateTime date, List<(int Priority, Venue Venue)> venuePriority);
}