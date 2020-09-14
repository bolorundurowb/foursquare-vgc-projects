using System;

namespace api.Data.DTOs
{
    public class DateSummaryDto
    {
        public DateTime Date { get; set; }

        public string HumanReadableDate => Date.ToString("ddd, dd MMM yyyy");

        public int NumOfEntries { get; set; }
    }
}
