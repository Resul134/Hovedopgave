using System;
using System.Collections.Generic;
using System.Text;

namespace Library.models
{
    public class User
    {

        private int _id;
        private string _username;
        private string _password;
        private int _age;
        private DateTime _date;
        private double _rating;
        private bool _suspended;

        public User()
        {
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public double Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        public bool Suspended
        {
            get { return _suspended; }
            set { _suspended = value; }
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Username)}: {Username}, {nameof(Password)}: {Password}, {nameof(Age)}: {Age}, {nameof(Date)}: {Date}, {nameof(Rating)}: {Rating}, {nameof(Suspended)}: {Suspended}";
        }
    }
}
