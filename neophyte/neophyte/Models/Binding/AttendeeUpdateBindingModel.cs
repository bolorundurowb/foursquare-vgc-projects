using System;

namespace neophyte.Models.Binding
{
    public class AttendeeUpdateBindingModel : AttendeeBindingModel
    {
        public DateTime? Date { get; set; }
        
        public string SeatAssigned { get; set; }
        
        public string SeatType { get; set; }
    }
}
