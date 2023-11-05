using System;

namespace neophyte.api.Models.Binding;

public class AttendanceUpdateBindingModel : AttendeeRegistrationBindingModel
{
    public DateTime? Date { get; set; }

    public string SeatAssigned { get; set; } = null!;

    public string SeatType { get; set; } = null!;
}