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
    public class MySqlDbFilmRepository : IFilmRepository
    {
        public Film GetById(int id)
        {
            Film film = null;
            string commandString = @"SELECT * FROM film_information WHERE idfilm=@idfilm";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@idfilm", id);

                using MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int idFilm = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string category = reader.GetString(2);
                    string annotation = reader.GetString(3);
                    DateTime releaseDate = reader.GetDateTime(4);
                    int purchasePrice = reader.GetInt32(5);
                    int rentalPrice = reader.GetInt32(6);
                    int rentalDuration = reader.GetInt32(7);
                    string ageRestriction = reader.GetString(8);
                    float middleRating = reader.GetFloat(9);
                    int ratingAmount = reader.GetInt32(10);

                    film = new Film(idFilm, title, category, annotation, releaseDate, purchasePrice, 
                                    rentalPrice, rentalDuration, ageRestriction, middleRating, ratingAmount);
                }
                return film;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public IEnumerable<FilmToLibrary> GetByUser(long iduser)
        {
            List<FilmToLibrary> films = new List<FilmToLibrary>();
            string commandString = @"
                            SELECT f.idfilm, f.title, f.category, f.release_date,
		                    fto.`type`, TIMESTAMPDIFF(HOUR, NOW(), DATE_ADD(o.`date`, INTERVAL f.rental_duration HOUR))
                            FROM films AS f
                            JOIN film_to_order AS fto ON f.idfilm=fto.idfilm
                            JOIN orders AS o ON fto.idorder=o.idorder
                            WHERE o.iduser = @iduser AND 
                            (fto.`type` = 'Покупка' OR 
                            DATE_ADD(o.`date`, INTERVAL f.rental_duration HOUR) > NOW())";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();

            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@iduser", iduser);

                using MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string category = reader.GetString(2);
                    DateTime releaseDate = reader.GetDateTime(3);
                    string type = reader.GetString(4);
                    int timeLeft = reader.GetInt32(5);

                    Film film = new Film(id, title, category, releaseDate);
                    films.Add(new FilmToLibrary(film, type, timeLeft));
                }

                return films;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public IEnumerable<Film> GetPopular()
        {
            List<Film> films = new List<Film>();
            string commandString = @"
                            SELECT f.idfilm, f.title, f.category, f.release_date
                            FROM films AS f
                            JOIN film_to_order ON film_to_order.idfilm=f.idfilm
                            GROUP BY (f.idfilm)
                            ORDER BY COUNT(f.idfilm) DESC
                            LIMIT 100";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();

            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                using MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string category = reader.GetString(2);
                    DateTime releaseDate = reader.GetDateTime(3);

                    films.Add(new Film(id, title, category, releaseDate));
                }

                return films;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public IEnumerable<Film> GetByGenre(int idgenre)
        {
            List<Film> films = new List<Film>();
            string commandString = @"
                            SELECT f.idfilm, f.title, f.category, f.release_date
                            FROM films AS f
                            JOIN genres_to_film ON genres_to_film.idfilm=f.idfilm
                            JOIN genres ON genres.idgenre=genres_to_film.idgenre
                            WHERE genres.idgenre=@idgenre
                            LIMIT 100";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();

            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@idgenre", idgenre);

                using MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string category = reader.GetString(2);
                    DateTime releaseDate = reader.GetDateTime(3);

                    films.Add(new Film(id, title, category, releaseDate));
                }

                return films;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public bool HasInLibrary(int idfilm, long iduser)
        {
            string commandString = @"
                             SELECT f.idfilm
                             FROM films AS f
                             JOIN film_to_order ON film_to_order.idfilm=f.idfilm
                             JOIN orders ON orders.idorder=film_to_order.idorder
                             WHERE f.idfilm=@idfilm AND orders.iduser=@iduser AND 
                             (film_to_order.`type` = 'Покупка' OR 
                             DATE_ADD(orders.`date`, INTERVAL f.rental_duration HOUR) > NOW())";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@idfilm", idfilm);
                command.Parameters.AddWithValue("@iduser", iduser);

                using MySqlDataReader reader = command.ExecuteReader();

                return reader.HasRows;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public IEnumerable<Film> GetByTitle(string title)
        {
            List<Film> films = new List<Film>();
            string commandString = @"
                                    SELECT f.idfilm, f.title, f.category, f.release_date
                                    FROM films AS f
                                    WHERE f.title LIKE (@title)
                                    UNION
                                    SELECT f.idfilm, f.title, f.category, f.release_date
                                    FROM films AS f
                                    WHERE f.title LIKE (@patern)";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            title = title.Replace(' ', '_');

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@title", title + '%');
                command.Parameters.AddWithValue("@patern", '%' + title + '%');

                using MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string filmTitle = reader.GetString(1);
                    string category = reader.GetString(2);
                    DateTime releaseDate = reader.GetDateTime(3);

                    films.Add(new Film(id, filmTitle, category, releaseDate));
                }

                return films;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }
    }
}
