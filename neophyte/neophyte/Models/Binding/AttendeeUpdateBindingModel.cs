using System;

namespace neophyte.Models.Binding
{
    public class AttendeeUpdateBindingModel : AttendeeBindingModel
    {
        public DateTime? Date { get; set; }
        
        public int? SeatNumber { get; set; }
    }
}