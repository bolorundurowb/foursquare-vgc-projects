using System;

namespace neophyte.api.Data.DTOs;

public record AttendanceSummaryDto(string Id, DateOnly Date, int NoOfAttendees);

public record AttendeeSummaryDto(string FirstName, string LastName, string? EmailAddress, string? PhoneNumber);
