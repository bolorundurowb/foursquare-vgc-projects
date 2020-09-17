using System.Collections.Generic;

namespace neophyte
{
    public static class Constants
    {
        public const string BaseUrl = "https://neophyte-dev-api.herokuapp.com/v1";
        
        public static List<string> Months = new List<string>
        {
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
            "November", "December"
        };

        public static List<string> AgeGroups = new List<string>
        {
            "11 - 17", "18 - 24", "25 - 30", "31 - 35", "36 - 40", "41 - 45", "46 - 50", "51 - 55", "56 - 60",
            "61 and Above"
        };

        public static List<string> Genders = new List<string>
        {
            "Other", "Male", "Female"
        };
    }
}
