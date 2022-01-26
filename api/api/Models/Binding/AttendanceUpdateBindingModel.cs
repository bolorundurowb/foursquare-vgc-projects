using System;

namespace api.Models.Binding;

public class AttendanceUpdateBindingModel :  AttendeeRegistrationBindingModel
{
    public DateTime? Date { get; set; }

    public string SeatAssigned { get; set; }

    public string SeatType { get; set; }
}