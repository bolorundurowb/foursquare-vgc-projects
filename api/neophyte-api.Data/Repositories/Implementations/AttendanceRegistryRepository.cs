using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meerkat;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using neophyte.api.Data.DTOs;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Repositories.Interfaces;

namespace neophyte.api.Data.Repositories.Implementations;

public class AttendanceRegistryRepository : IAttendanceRegistryRepository
{
    public Task<List<AttendanceSummaryDto>> GetAll(int skip, int limit)
    {
        return Meerkat.Query<AttendanceRegistry>()
            .OrderByDescending(x => x.Date)
            .Select(x => new AttendanceSummaryDto(x.Date, x.Attendees.Count))
            .Skip(skip)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<AttendeeSummaryDto>> GetAttendeesForRegistry(string registryId)
    {
        var registry = await Meerkat.FindByIdAsync<AttendanceRegistry>(registryId);

        if (registry is null)
            throw new Exception();

        return registry.Attendees
            .Select(x => new AttendeeSummaryDto(x.FirstName, x.LastName, x.EmailAddress, x.PhoneNumber))
            .ToList();
    }

    public async Task Create(string firstName, string lastName, string? emailAddress, string? phoneNumber,
        string? seatNumber)
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        var registry = await Meerkat.FindOneAsync<AttendanceRegistry>(x => x.Date == date);

        if (registry is null)
        {
            registry = new AttendanceRegistry(date);
            await registry.SaveAsync();
        }

        registry.AddAttendee(firstName, lastName, emailAddress, phoneNumber, seatNumber);
        await registry.SaveAsync();
    }
}
