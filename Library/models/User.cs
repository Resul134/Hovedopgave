using System;
using System.Collections.Generic;
using System.Text;

namespace Library.models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public double Rating { get; set; }
        public bool Suspended { get; set; }

        public User()
        {
        }

        public override string ToString()
        {
            return $"{nameof(ID)}: {ID}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Birthday)}: {Birthday}, {nameof(Gender)}: {Gender}, {nameof(Phone)}: {Phone}, {nameof(Email)}: {Email}, {nameof(Username)}: {Username}, {nameof(Password)}: {Password}, {nameof(Age)}: {Age}, {nameof(Date)}: {Date}, {nameof(Rating)}: {Rating}, {nameof(Suspended)}: {Suspended}";
        }
    }
}
