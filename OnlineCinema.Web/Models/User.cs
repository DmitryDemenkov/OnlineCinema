using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web.Models
{
    public class User
    {
        public User(string login, string password, string email, DateTime birthDate = default, long id = 0) 
        {
            Id = id;
            Login = login;
            Password = password;
            Email = email;
            BirthDate = birthDate;
        }

        public long Id { get; private set; }

        public string Login { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
