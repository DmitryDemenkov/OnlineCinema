using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.RepositoryInterfaces;
using OnlineCinema.Web.SqlDbUtils;

namespace OnlineCinema.Web.Repositories
{
    public class MySqlDbUserRepository : IUserRepository
    {
        public User Append(User newUser)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByLogin(string login, string password)
        {
            User user = null;
            string commandString = @$"
                            SELECT iduser, email, birth_date
                            FROM users
                            WHERE login='$login' AND `password`='$password'";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();

            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("$login", login);
                command.Parameters.AddWithValue("$password", password);

                using MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int idUser = reader.GetInt32(0);
                    string email = reader.GetString(1);
                    DateTime birthdate = reader.GetDateTime(2);

                    user = new User(idUser, login, password, email, birthdate);
                }
                
                return user;
            }
            catch (MySqlException exception)
            {
                throw new OnlineCinemaException(exception.Number, exception.Message);
            }
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
