using System.Collections.Generic;
using System.Threading.Tasks;
using neophyte.api.Data.DTOs;

namespace neophyte.api.Data.Repositories.Interfaces;

public interface IAttendanceRegistryRepository
{
    Task<List<AttendanceSummaryDto>> GetAll(int skip, int limit);

    Task<List<AttendeeSummaryDto>> GetAttendeesForRegistry(string registryId);

    Task Create(string firstName, string lastName, string? emailAddress, string? phoneNumber,
        string? seatNumber);
}
