using System;

namespace neophyte;

public class AttendanceUpdateBindingModel :  AttendeeRegistrationBindingModel
{
    public DateTime? Date { get; set; }

    public string SeatAssigned { get; set; }

    public string SeatType { get; set; }
}