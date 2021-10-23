using System;

namespace neophyte.Models.View
{
    public class AttendeeViewModel
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public string EmailAddress { get; set; }

        public string FullName { get; set; }

        public int? Age { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string ResidentialAddress { get; set; }

        public bool ReturnedInLastTenDays { get; set; }

        public bool LiveWithCovidCaregivers { get; set; }

        public bool CaredForSickPerson { get; set; }

        public string HaveCovidSymptoms { get; set; }

        public int? SeatNumber { get; set; }

        public string SeatAssigned { get; set; }

        public string SeatType { get; set; }
    }
}
