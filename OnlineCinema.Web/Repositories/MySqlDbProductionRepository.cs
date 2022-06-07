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
    public class MySqlDbProductionRepository : IProductionRepository
    {
        public IEnumerable<Production> GetByFilm(Film film)
        {
            List<Production> productions = new List<Production>();
            string commandString = @"
                            SELECT persons.idperson, persons.`name`, persons.information,
	                               posts.idpost, posts.`name`
                            FROM productions
                            JOIN persons ON persons.idperson=productions.idperson
                            JOIN posts ON posts.idpost=productions.idpost
                            JOIN films ON films.idfilm=productions.idfilm
                            WHERE films.idfilm=@idfilm
                            ORDER BY posts.`name`";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@idfilm", film.Id);

                using MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int idperson = reader.GetInt32(0);
                    string personName = reader.GetString(1);
                    string personInformation = reader.GetString(2);
                    int idpost = reader.GetInt32(3);
                    string postName = reader.GetString(4);

                    Person person = new Person(idperson, personName, personInformation);
                    Post post = new Post(idpost, postName);

                    productions.Add(new Production(film, person, post));
                }

                return productions;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public IEnumerable<Production> GetByPerson(Person person)
        {
            List<Production> productions = new List<Production>();
            string commandString = @"
                            SELECT films.idfilm, films.title, films.category,
	                               films.release_date, posts.idpost, posts.`name`
                            FROM productions
                            JOIN persons ON persons.idperson=productions.idperson
                            JOIN posts ON posts.idpost=productions.idpost
                            JOIN films ON films.idfilm=productions.idfilm
                            WHERE persons.idperson=@idperson
                            ORDER BY films.release_date DESC";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@idperson", person.Id);

                using MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int idfilm = reader.GetInt32(0);
                    string filmTitle = reader.GetString(1);
                    string category = reader.GetString(2);
                    DateTime releaseDate = reader.GetDateTime(3);
                    int idpost = reader.GetInt32(4);
                    string postName = reader.GetString(5);

                    Film film = new Film(idfilm, filmTitle, category, releaseDate);
                    Post post = new Post(idpost, postName);

                    productions.Add(new Production(film, person, post));
                }

                return productions;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }
    }
}
