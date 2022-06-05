﻿using System;
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
            throw new NotImplementedException();
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
    }
}
