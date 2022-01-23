using api.Data.Entities;
using CsvHelper.Configuration;

namespace api.Data.CsvMappers
{
    public sealed class AttendeeCsvMapper : ClassMap<Attendee>
    {
        public AttendeeCsvMapper()
        {
            Map(x => x.SerialNo)
                .Name("S/No")
                .Index(0);

            Map(x => x.FullName)
                .Name("Full Name")
                .Index(1);

            Map(x => x.Phone)
                .Name("Phone Number")
                .Index(2);

            Map(x => x.SeatType)
                .Name("Seat Type")
                .Index(3)
                .Convert(x => x.Value.SeatType);

            Map(x => x.SeatNumber)
                .Name("Seat Number")
                .Index(4)
                .Convert(x => x.Value.SeatNumber.HasValue ? x.Value.SeatNumber.Value.ToString() : x.Value.SeatAssigned);
        }
    }
}