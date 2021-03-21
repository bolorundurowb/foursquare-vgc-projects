using api.Data.Models;
using CsvHelper.Configuration;

namespace api.Data.CsvMappers
{
    public class NewcomerCsvMapper : ClassMap<Newcomer>
    {
        public NewcomerCsvMapper()
        {
            Map(x => x.SerialNo)
                .Name("S/No")
                .Index(0)
                .Convert(x => x.Row.Parser.Row + 1);
            
            Map(x => x.FullName)
                .Name("Full Name")
                .Index(1);

            Map(x => x.EmailAddress)
                .Name("Email Address")
                .Index(2);

            Map(x => x.Phone)
                .Name("Phone Number")
                .Index(3);
            
            Map(x => x.HomeAddress)
                .Name("Residential Address")
                .Index(4);
            
            Map(x => x.BornAgain)
                .Name("Are you born again?")
                .Index(5)
                .Convert(x => x.Value.BornAgain.GetValueOrDefault().ToString());
            
            Map(x => x.BecomeMember)
                .Name("Do you want to become a member of this church?")
                .Index(6)
                .Convert(x => x.Value.BecomeMember.GetValueOrDefault().ToString());
            
            Map(x => x.AgeGroup)
                .Name("Age Group")
                .Index(7);
            
            Map(x => x.BirthDay)
                .Name("Birthday")
                .Index(8);

            Map(x => x.CommentsOrPrayers)
                .Name("Comments/Prayers")
                .Index(9);

            Map(x => x.HowYouFoundUs)
                .Name("How did you find out about us?")
                .Index(10);

            Map(x => x.Remarks)
                .Name("Remarks")
                .Index(11);
        }
    }
}
