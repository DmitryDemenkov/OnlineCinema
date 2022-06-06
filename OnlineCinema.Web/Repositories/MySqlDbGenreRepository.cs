using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.RepositoryInterfaces;
using OnlineCinema.Web.SqlDbUtils;
using MySql.Data.MySqlClient;

namespace OnlineCinema.Web.Repositories
{
    public class MySqlDbGenreRepository : IGenreRepository
    {
        public IEnumerable<Genre> GetByFilm(int idfilm)
        {
            List<Genre> genres = new List<Genre>();
            string commandString = @"
                                SELECT genres.idgenre, genres.`name` 
                                FROM genres 
                                JOIN genres_to_film ON genres.idgenre=genres_to_film.idgenre
                                JOIN films ON genres_to_film.idfilm=films.idfilm
                                WHERE films.idfilm=@idfilm";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@idfilm", idfilm);

                using MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int idGenre = reader.GetInt32(0);
                    string genreName = reader.GetString(1);

                    genres.Add(new Genre(idGenre, genreName));
                }
                return genres;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }
    }
}
