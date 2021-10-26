using meerkat;
using meerkat.Attributes;

namespace api.Data.Models
{
    [Collection(TrackTimestamps = true)]
    public class Person : Schema
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        private Person()
        {
        }

        public Person(string firstName, string lastName, string phone)
        {
            FirstName = firstName?.Trim();
            LastName = lastName?.Trim();
            Phone = phone?.Normalize();
        }
    }
}