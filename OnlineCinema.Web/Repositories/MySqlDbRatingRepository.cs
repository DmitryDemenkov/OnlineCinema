using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.RepositoryInterfaces;
using MySql.Data.MySqlClient;
using OnlineCinema.Web.SqlDbUtils;

namespace OnlineCinema.Web.Repositories
{
    public class MySqlDbRatingRepository : IRatingRepository
    {
        public Rating Append(Rating rating, int idfilm)
        {
            string commandString = @"
                        INSERT INTO ratings 
                        (iduser, idfilm, plot, `action`, actor_play, effects) 
                        VALUES (@iduser, @idfilm, @plot, @action, @actor_play, @effects)";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@iduser", rating.User.Id);
                command.Parameters.AddWithValue("@idfilm", idfilm);
                command.Parameters.AddWithValue("@plot", rating.Plot);
                command.Parameters.AddWithValue("@action", rating.Action);
                command.Parameters.AddWithValue("@actor_play", rating.ActorPlay);
                command.Parameters.AddWithValue("@effects", rating.Effects);

                command.ExecuteNonQuery();
                return rating;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public void Delete(long iduser, int idfilm)
        {
            string commandString = @"DELETE FROM ratings WHERE (idfilm=@idfilm) and (iduser=@iduser)";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@iduser", iduser);
                command.Parameters.AddWithValue("@idfilm", idfilm);

                command.ExecuteNonQuery();
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public IEnumerable<Rating> GetByFilm(int idfilm, long iduser = 0)
        {
            List<Rating> ratings = new List<Rating>();
            string commandString = @"
                            SELECT 'В среднем', '0', IFNULL(AVG(r.`action`), 0), IFNULL(AVG(r.actor_play), 0), IFNULL(AVG(r.plot), 0), IFNULL(AVG(r.effects), 0),
                                    IFNULL(AVG(ROUND((r.`action` +r.actor_play + r.plot + r.effects) / 4, 2)), 0) AS mid
                            FROM ratings AS r
                            WHERE idfilm=@idfilm
                            UNION
                            SELECT users.login, r.iduser, r.`action`, r.actor_play, r.plot, r.effects, 
		                            ROUND((r.`action` +r.actor_play + r.plot + r.effects) / 4, 2) AS mid
                            FROM ratings AS r
                            JOIN users ON r.iduser = users.iduser
                            WHERE idfilm=@idfilm AND r.iduser=@iduser
                            UNION
                            SELECT users.login, r.iduser, r.`action`, r.actor_play, r.plot, r.effects, 
		                            ROUND((r.`action` +r.actor_play + r.plot + r.effects) / 4, 2) AS mid
                            FROM ratings AS r
                            JOIN users ON r.iduser = users.iduser
                            WHERE idfilm=@idfilm";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@idfilm", idfilm);
                command.Parameters.AddWithValue("@iduser", iduser);

                using MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string userName = reader.GetString(0);
                    long userid = reader.GetInt64(1);
                    float action = reader.GetFloat(2);
                    float actorPlay = reader.GetFloat(3);
                    float plot = reader.GetFloat(4);
                    float effects = reader.GetFloat(5);
                    float middle = reader.GetFloat(6);

                    User user = new User(userid, userName);
                    ratings.Add(new Rating(user, action, actorPlay, plot, effects, middle));
                }
                
                return ratings;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public IEnumerable<Rating> GetByUser(long iduser)
        {
            List<Rating> ratings = new List<Rating>();
            string commandString = @"
                                    SELECT f.idfilm, f.title, f.category, f.release_date,
                                           r.`action`, r.actor_play, r.plot, r.effects, 
		                                   ROUND((r.`action` + r.actor_play + r.plot + r.effects) / 4, 2) AS mid
                                    FROM ratings AS r
                                    JOIN films AS f ON r.idfilm=f.idfilm
                                    WHERE r.iduser=@iduser";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@iduser", iduser);

                using MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int idfilm = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string category = reader.GetString(2);
                    DateTime releaseDate = reader.GetDateTime(3);
                    float action = reader.GetFloat(4);
                    float actorPlay = reader.GetFloat(5);
                    float plot = reader.GetFloat(6);
                    float effects = reader.GetFloat(7);
                    float middle = reader.GetFloat(8);

                    Film film = new Film(idfilm, title, category, releaseDate);
                    ratings.Add(new Rating(film, action, actorPlay, plot, effects, middle));
                }
                
                return ratings;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public Rating GetRating(int idfilm, long iduser)
        {
            Rating rating = null;
            string commandString = @"
                                    SELECT users.login, r.iduser, r.`action`, r.actor_play, r.plot, r.effects, 
		                                   ROUND((r.`action` + r.actor_play + r.plot + r.effects) / 4, 2) AS mid
                                    FROM ratings AS r
                                    JOIN users ON r.iduser=users.iduser
                                    WHERE idfilm=@idfilm AND r.iduser=@iduser";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@idfilm", idfilm);
                command.Parameters.AddWithValue("@iduser", iduser);

                using MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string userName = reader.GetString(0);
                    long userid = reader.GetInt64(1);
                    float action = reader.GetFloat(2);
                    float actorPlay = reader.GetFloat(3);
                    float plot = reader.GetFloat(4);
                    float effects = reader.GetFloat(5);
                    float middle = reader.GetFloat(6);

                    User user = new User(userid, userName);
                    rating = new Rating(user, action, actorPlay, plot, effects, middle);
                }

                return rating;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public Rating Update(Rating rating, int idfilm)
        {
            string commandString = @"
                        UPDATE ratings SET plot=@plot, `action`=@action, actor_play=@actor_play, effects=@effects 
                        WHERE (idfilm=@idfilm) AND (iduser=@iduser)";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@iduser", rating.User.Id);
                command.Parameters.AddWithValue("@idfilm", idfilm);
                command.Parameters.AddWithValue("@plot", rating.Plot);
                command.Parameters.AddWithValue("@action", rating.Action);
                command.Parameters.AddWithValue("@actor_play", rating.ActorPlay);
                command.Parameters.AddWithValue("@effects", rating.Effects);

                command.ExecuteNonQuery();
                return rating;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }
    }
}
