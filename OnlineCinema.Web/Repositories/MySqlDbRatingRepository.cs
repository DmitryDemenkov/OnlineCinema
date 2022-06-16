﻿using System;
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
        public IEnumerable<Rating> GetByFilm(int idfilm, long iduser = 0)
        {
            List<Rating> ratings = new List<Rating>();
            string commandString = @"
                            SELECT 'В среднем', AVG(r.`action`), AVG(r.actor_play), AVG(r.plot), AVG(r.effects), 
                                    AVG(ROUND((r.`action` +r.actor_play + r.plot + r.effects) / 4, 2)) AS mid
                            FROM ratings AS r
                            WHERE idfilm=@idfilm
                            UNION
                            SELECT users.login, r.`action`, r.actor_play, r.plot, r.effects, 
		                            ROUND((r.`action` +r.actor_play + r.plot + r.effects) / 4, 2) AS mid
                            FROM ratings AS r
                            JOIN users ON r.iduser = users.iduser
                            WHERE idfilm=@idfilm AND r.iduser=@iduser
                            UNION
                            SELECT users.login, r.`action`, r.actor_play, r.plot, r.effects, 
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
                    float action = reader.GetFloat(1);
                    float actorPlay = reader.GetFloat(2);
                    float plot = reader.GetFloat(3);
                    float effects = reader.GetFloat(4);
                    float middle = reader.GetFloat(5);

                    ratings.Add(new Rating(userName, action, actorPlay, plot, effects, middle));
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
            throw new NotImplementedException();
        }
    }
}