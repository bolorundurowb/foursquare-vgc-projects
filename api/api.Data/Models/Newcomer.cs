using System;
using api.Data.Enums;
using meerkat;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Data.Models
{
    public class Newcomer : Schema
    {
        public DateTime Date { get; private set; }

        public string FullName { get; private set; }

        public string HomeAddress { get; private set; }

        public string Phone { get; private set; }

        public string EmailAddress { get; private set; }

        public string BirthDay { get; private set; }

        public Gender? Gender { get; private set; }

        public string AgeGroup { get; private set; }

        public string CommentsOrPrayers { get; private set; }

        public string HowYouFoundUs { get; private set; }

        public MultiChoice? BornAgain { get; private set; }

        public MultiChoice? BecomeMember { get; private set; }

        public string Remarks { get; private set; }
        
        [BsonIgnore]
        public int SerialNo { get; set; }

        private Newcomer()
        {
        }

        public Newcomer(string fullName, string homeAddress, string phone, string email, string birthDay,
            Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs, MultiChoice? bornAgain,
            MultiChoice? becomeMember, string remarks = null)
        {
            Date = DateTime.UtcNow.Date;
            FullName = fullName;
            HomeAddress = homeAddress;
            Phone = phone;
            EmailAddress = email;
            BirthDay = birthDay;
            Gender = gender;
            AgeGroup = ageGroup;
            CommentsOrPrayers = commentsOrPrayers;
            HowYouFoundUs = howYouFoundUs;
            BornAgain = bornAgain;
            BecomeMember = becomeMember;
            Remarks = remarks ?? string.Empty;
        }

        public void UpdateDate(DateTime? date)
        {
            if (date.HasValue)
            {
                Date = date.Value.Date;
            }
        }

        public void UpdateFullName(string fullName)
        {
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                FullName = fullName;
            }
        }

        public void UpdateHomeAddress(string homeAddress)
        {
            if (!string.IsNullOrWhiteSpace(homeAddress))
            {
                HomeAddress = homeAddress;
            }
        }

        public void UpdatePhone(string phone)
        {
            if (!string.IsNullOrWhiteSpace(phone))
            {
                Phone = phone;
            }
        }

        public void UpdateEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                EmailAddress = email;
            }
        }

        public void UpdateBirthDay(string birthDay)
        {
            if (!string.IsNullOrWhiteSpace(birthDay))
            {
                BirthDay = birthDay;
            }
        }

        public void UpdateAgeGroup(string agGroup)
        {
            if (!string.IsNullOrWhiteSpace(agGroup))
            {
                AgeGroup = agGroup;
            }
        }

        public void UpdateCommentsOrPrayers(string commentsOrPrayers)
        {
            if (!string.IsNullOrWhiteSpace(commentsOrPrayers))
            {
                CommentsOrPrayers = commentsOrPrayers;
            }
        }

        public void UpdateHowYouFoundUs(string howYouFoundUs)
        {
            if (!string.IsNullOrWhiteSpace(howYouFoundUs))
            {
                HowYouFoundUs = howYouFoundUs;
            }
        }

        public void UpdateRemarks(string remarks)
        {
            if (!string.IsNullOrWhiteSpace(remarks))
            {
                Remarks = remarks;
            }
        }

        public void UpdateGender(Gender? gender)
        {
            if (gender.HasValue)
            {
                Gender = gender.Value;
            }
        }

        public void UpdateBornAgain(MultiChoice? bornAgain)
        {
            if (bornAgain.HasValue)
            {
                BornAgain = bornAgain.Value;
            }
        }

        public void UpdateBecomeMember(MultiChoice? becomeMember)
        {
            if (becomeMember.HasValue)
            {
                BecomeMember = becomeMember.Value;
            }
        }
    }
}
