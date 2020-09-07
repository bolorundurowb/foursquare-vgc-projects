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

        private Attendee()
        {
        }

        public Attendee(string fullName, string homeAddress, string phone, string email, string birthDay,
            Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs, MultiChoice? bornAgain,
            MultiChoice? becomeMember, string remarks)
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
            Remarks = remarks;
        }
    }
}