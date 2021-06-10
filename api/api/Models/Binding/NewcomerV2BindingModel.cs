using api.Data.Enums;

namespace api.Models.Binding
{
    public class NewcomerV2BindingModel
    {
        public string FullName { get; set; }

        public string HomeAddress { get; set; }

        public string Phone { get; set; }

        public string EmailAddress { get; set; }

        public string BirthDay { get; set; }

        public string AgeGroup { get; set; }

        public string CommentsOrPrayers { get; set; }

        public string HowYouFoundUs { get; set; }

        public MultiChoice? BecomeMember { get; set; }
    }
}