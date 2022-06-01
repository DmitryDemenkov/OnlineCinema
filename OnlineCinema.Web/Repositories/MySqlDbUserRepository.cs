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
            User user = null;
            string commandString = "INSERT INTO users ";

            if (newUser.BirthDate != default)
                commandString += "(login, `password`, email, birth_date) VALUES (@login, @password, @email, @birth_date)";

            else
                commandString += "(login, `password`, email) VALUES (@login, @password, @email)";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@login", newUser.Login);
                command.Parameters.AddWithValue("@password", newUser.Password);
                command.Parameters.AddWithValue("@email", newUser.Email);
                command.Parameters.AddWithValue("@birth_date", newUser.BirthDate.ToString("yyyy-MM-dd"));

                command.ExecuteNonQuery();
                long userId = command.LastInsertedId;

                user = new User(newUser.Login, newUser.Password, newUser.Email, newUser.BirthDate, userId);
                return user;
            }
            catch (MySqlException exception)
            {
                int errorCode = exception.Number;
                
                if (errorCode == 1062 && exception.Message.Contains("login_UNIQUE"))
                    errorCode = -1;

                if (errorCode == 1062 && exception.Message.Contains("email_UNIQUE"))
                    errorCode = -2;

                throw new RepositoryException(errorCode, exception.Message);
            }
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
                            WHERE login=@login AND `password`=@password";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();

            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);

                using MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int idUser = reader.GetInt32(0);
                    string email = reader.GetString(1);
                    DateTime birthdate = reader.GetDateTime(2);

                    user = new User(login, password, email, birthdate, idUser);
                }
                
                return user;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
