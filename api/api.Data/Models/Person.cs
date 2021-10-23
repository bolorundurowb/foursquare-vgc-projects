using meerkat;

namespace api.Data.Models
{
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
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
    }
}