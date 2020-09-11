using System;
using api.Data.Enums;
using moment.net;
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

        public int? Age { get; private set; }

        public Gender? Gender { get; private set; }

        public string Phone { get; private set; }

        public string ResidentialAddress { get; private set; }

        public bool ReturnedInLastTenDays { get; private set; }

        public bool LiveWithCovidCaregivers { get; private set; }

        public bool CaredForSickPerson { get; private set; }

        public MultiChoice? HaveCovidSymptoms { get; private set; }

        public int? SeatNumber { get; private set; }

        private Attendee()
        {
        }

        public Attendee(string emailAddress, string fullName, int? age, string phone, string residentialAddress,
            Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers, bool caredForSickPerson,
            MultiChoice? haveCovidSymptoms)
        {
            // they should registering against the next sunday
            Date = DateTime.Now.Next(DayOfWeek.Sunday);

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

        public void UpdateResidentialAddress(string residentialAddress)
        {
            if (!string.IsNullOrWhiteSpace(residentialAddress))
            {
                ResidentialAddress = residentialAddress;
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

        public void UpdateGender(Gender? gender)
        {
            if (gender.HasValue)
            {
                Gender = gender.Value;
            }
        }

        public void UpdateHaveCovidSymptoms(MultiChoice? haveCovidSymptoms)
        {
            if (haveCovidSymptoms.HasValue)
            {
                HaveCovidSymptoms = haveCovidSymptoms.Value;
            }
        }

        public void UpdateAge(int? seatNumber)
        {
            if (seatNumber.HasValue)
            {
                Age = seatNumber.Value;
            }
        }

        public void UpdateSeatNumber(int? seatNumber)
        {
            if (seatNumber.HasValue)
            {
                SeatNumber = seatNumber.Value;
            }
        }

        public void UpdateReturnedInLastTenDays(bool? returnedInLastTen)
        {
            if (returnedInLastTen.HasValue)
            {
                ReturnedInLastTenDays = returnedInLastTen.Value;
            }
        }

        public void UpdateLiveWithCovidCaregivers(bool? liveWithCaregivers)
        {
            if (liveWithCaregivers.HasValue)
            {
                LiveWithCovidCaregivers = liveWithCaregivers.Value;
            }
        }

        public void UpdateCaredForSickPerson(bool? caredForSickPerson)
        {
            if (caredForSickPerson.HasValue)
            {
                CaredForSickPerson = caredForSickPerson.Value;
            }
        }
    }
}
