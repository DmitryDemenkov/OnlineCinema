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

        public User AddNewUser(string login, string password, string email,
                               out int errorCode, DateTime birthDate = default)
        {
            User newUser = new User(login, password, email, birthDate);
            try
            {
                User user = userRepository.Append(newUser);
                errorCode = 0;
                return user;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }

        public User UpdateUser(User user, out int errorCode)
        {
            try
            {
                User updatedUser = userRepository.Update(user);
                errorCode = 0;
                return updatedUser;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }
    }
}
