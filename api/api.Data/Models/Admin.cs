using System;
using meerkat;

namespace api.Data.Models
{
    public class Admin : Schema
    {
        public string Name { get; private set; }

        public string EmailAddress { get; private set; }

        public DateTime AddedAt { get; private set; }

        private Admin()
        {
        }

        public Admin(string name, string emailAddress)
        {
            AddedAt = DateTime.UtcNow;
            Name = name;
            EmailAddress = emailAddress;
        }
    }
}