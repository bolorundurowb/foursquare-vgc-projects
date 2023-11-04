using System;
using neophyte.api.Data.Enums;

namespace neophyte.api.Models.View;

public class AttendeeViewModel
{
    public string Id { get; set; } = null!;

    public DateTime Date { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int? Age { get; set; }

    public Gender? Gender { get; set; }

    public string Phone { get; set; } = null!;

    public string ResidentialAddress { get; set; } = null!;

    public bool ReturnedInLastTenDays { get; set; }

    public bool LiveWithCovidCaregivers { get; set; }

    public bool CaredForSickPerson { get; set; }

    public MultiChoice? HaveCovidSymptoms { get; set; }

    public int? SeatNumber { get; set; }

    public string SeatAssigned { get; set; } = null!;

    public string SeatType { get; set; } = null!;
}