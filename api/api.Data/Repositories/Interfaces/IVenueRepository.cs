using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Enums;

namespace api.Data.Repositories.Interfaces;

public interface IVenueRepository
{
    Task<List<Venue>> GetAll();
    
    Task<Venue> FindByName(string name);
    
    Task<Venue> Create(string name, List<(SeatCategory Category, string Range)> seatRanges);
}