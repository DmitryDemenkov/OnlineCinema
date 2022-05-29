using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.RepositoryInterfaces
{
    interface IUserRepository : IRepository<User>
    {
        public User GetByLogin(string login, string password);

        public User Append(User newUser);

        public User Update(User user);

        public void Delete(User user);
    }
}
