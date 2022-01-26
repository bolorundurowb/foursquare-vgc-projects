using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Enums;

namespace api.Data.Repositories.Interfaces;

public interface IVenueRepository
{
    Task<Venue> Create(string name, List<(SeatCategory Category, string Range)> seatRanges);
}