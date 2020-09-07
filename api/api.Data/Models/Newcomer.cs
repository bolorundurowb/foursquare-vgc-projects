using System;
using api.Data.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Data.Models
{
    public class Newcomer
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

        private Newcomer()
        {
        }

        public Newcomer(string emailAddress, string fullName, string age, string phone, string residentialAddress,
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
    }
}