using System;

namespace api.Models.Binding
{
    public class AttendanceUpdateBindingModel :  AttendeeRegistrationBindingModel
    {
        public DateTime? Date { get; set; }

        public int? SeatNumber { get; set; }
    }
}
