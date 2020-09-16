using api.Data.Models;
using CsvHelper.Configuration;

namespace api.Data.CsvMappers
{
    public class AttendeeCsvMapper : ClassMap<Attendee>
    {
        public AttendeeCsvMapper()
        {
            Map(x => x.FullName)
                .Name("Full Name")
                .Index(0);

            Map(x => x.EmailAddress)
                .Name("Email Address")
                .Index(1);

            Map(x => x.Phone)
                .Name("Phone Number")
                .Index(2);

            Map(x => x.ResidentialAddress)
                .Name("ResidentialAddress")
                .Index(3);

            Map(x => x.SeatNumber)
                .Name("Seat Number")
                .Index(4);

            Map(x => x.Gender)
                .Name("Gender")
                .Index(5)
                .ConvertUsing(x => x.Gender.GetValueOrDefault().ToString());

            Map(x => x.ReturnedInLastTenDays)
                .Name("Did you return from an overseas trip in the last 10 days?")
                .Index(6)
                .ConvertUsing(x => x.ReturnedInLastTenDays ? "Yes" : "No");

            Map(x => x.LiveWithCovidCaregivers)
                .Name("Do you live with COVID-19 caregivers?")
                .Index(7)
                .ConvertUsing(x => x.LiveWithCovidCaregivers ? "Yes" : "No");

            Map(x => x.CaredForSickPerson)
                .Name("Have you cared for any sick person recently?")
                .Index(8)
                .ConvertUsing(x => x.CaredForSickPerson ? "Yes" : "No");

            Map(x => x.HaveCovidSymptoms)
                .Name("Do you have a cough, catarrh or sore-throat??")
                .Index(9)
                .ConvertUsing(x => x.HaveCovidSymptoms.GetValueOrDefault().ToString());
        }
    }
}
