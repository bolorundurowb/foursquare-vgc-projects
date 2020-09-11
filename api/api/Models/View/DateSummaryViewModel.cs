using System;

namespace api.Models.View
{
    public class DateSummaryViewModel
    {
        public DateTime Date { get; set; }

        public string HumanReadableDate { get; set; }

        public int NumOfEntries { get; set; }
    }
}
