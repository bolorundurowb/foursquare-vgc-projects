﻿using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Enums;

namespace api.Data.Repositories.Interfaces;

public interface IVenueRepository
{
    Task<List<Venue>> GetAll();

    Task<Dictionary<string, Venue>> FindAndMapById(IEnumerable<string> venueIds);

    Task<Venue> FindById(string venueId);

    Task<Venue> FindByName(string name);

    Task<Venue> Create(string name, List<(SeatCategory Category, string Range)> seatRanges);
}