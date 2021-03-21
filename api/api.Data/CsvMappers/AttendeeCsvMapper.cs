using api.Data.Models;
using CsvHelper.Configuration;

namespace api.Data.CsvMappers
{
    public class AttendeeCsvMapper : ClassMap<Attendee>
    {
        public AttendeeCsvMapper()
        {
            Map(x => x.SerialNo)
                .Name("S/No")
                .Index(0)
                .Convert(x => x.Row.Parser.Row);

            Map(x => x.FullName)
                .Name("Full Name")
                .Index(1);

            Map(x => x.EmailAddress)
                .Name("Email Address")
                .Index(2);

            Map(x => x.Phone)
                .Name("Phone Number")
                .Index(3);

            Map(x => x.ResidentialAddress)
                .Name("ResidentialAddress")
                .Index(4);

            Map(x => x.SeatNumber)
                .Name("Seat Number")
                .Index(5);

            Map(x => x.Gender)
                .Name("Gender")
                .Index(6)
                .Convert(x => x.Value.Gender.GetValueOrDefault().ToString());

            Map(x => x.ReturnedInLastTenDays)
                .Name("Did you return from an overseas trip in the last 10 days?")
                .Index(7)
                .Convert(x => x.Value.ReturnedInLastTenDays ? "Yes" : "No");

            Map(x => x.LiveWithCovidCaregivers)
                .Name("Do you live with COVID-19 caregivers?")
                .Index(8)
                .Convert(x => x.Value.LiveWithCovidCaregivers ? "Yes" : "No");

            Map(x => x.CaredForSickPerson)
                .Name("Have you cared for any sick person recently?")
                .Index(9)
                .Convert(x => x.Value.CaredForSickPerson ? "Yes" : "No");

            Map(x => x.HaveCovidSymptoms)
                .Name("Do you have a cough, catarrh or sore-throat??")
                .Index(10)
                .Convert(x => x.Value.HaveCovidSymptoms.GetValueOrDefault().ToString());
        }
    }
}