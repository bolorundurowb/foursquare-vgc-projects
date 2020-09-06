using System;
namespace neophyte.Models
{
    public class DateEntry
    {
        public string LongDate => DateTime.Parse(Date).ToString("ddd, MMM dd, yyyy");

        public string Date { get; set; }
    }
}
