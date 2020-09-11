using System;

namespace api.Data.DTOs
{
    public class DateSummaryDto
    {
        public DateTime Date { get; set; }

        public string HumanReadableDate => Date.ToString("ddd, MMM yyyy h:mm tt");

        public int NumOfEntries { get; set; }
    }
}
