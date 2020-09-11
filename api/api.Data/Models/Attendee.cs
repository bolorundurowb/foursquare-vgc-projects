using System;
using api.Data.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Data.Models
{
    public class Attendee : IEntity
    {
        [BsonId] public ObjectId Id { get; private set; }

        public DateTime Date { get; private set; }

        public string EmailAddress { get; private set; }

        public string FullName { get; private set; }

        public string Age { get; private set; }

        public Gender? Gender { get; private set; }

        public string Phone { get; private set; }

        public string ResidentialAddress { get; private set; }

        public bool ReturnedInLastTenDays { get; private set; }

        public bool LiveWithCovidCaregivers { get; private set; }

        public bool CaredForSickPerson { get; private set; }

        public MultiChoice? HaveCovidSymptoms { get; private set; }

        public int SeatNumber { get; private set; }

        private Attendee()
        {
        }

        public Attendee(string emailAddress, string fullName, string age, string phone, string residentialAddress,
            Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers, bool caredForSickPerson,
            MultiChoice? haveCovidSymptoms, int seatNumber = 0)
        {
            Date = DateTime.UtcNow.Date;
            EmailAddress = emailAddress;
            FullName = fullName;
            Age = age;
            Phone = phone;
            ResidentialAddress = residentialAddress;
            Gender = gender;
            ReturnedInLastTenDays = returnedInLastTenDays;
            LiveWithCovidCaregivers = liveWithCovidCaregivers;
            CaredForSickPerson = caredForSickPerson;
            HaveCovidSymptoms = haveCovidSymptoms;
            SeatNumber = seatNumber;
        }

        public void UpdateDate(DateTime? date)
        {
            if (date.HasValue)
            {
                Date = date.Value;
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
