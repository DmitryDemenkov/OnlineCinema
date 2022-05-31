using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Repositories;
using OnlineCinema.Web.RepositoryInterfaces;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.Services
{
    public class DbUserService
    {
        public DbUserService()
        {
            userRepository = new MySqlDbUserRepository();
        }

        private IUserRepository userRepository;

        public User GetByLogin(string login, string password, out int errorCode)
        {
            try
            {
                User user = userRepository.GetByLogin(login, password);
                errorCode = 0;
                return user;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }
    }
}
