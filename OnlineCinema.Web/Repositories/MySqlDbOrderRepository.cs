﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using OnlineCinema.Web.SqlDbUtils;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.RepositoryInterfaces;

namespace OnlineCinema.Web.Repositories
{
    public class MySqlDbOrderRepository : IOrderRepository
    {
        public Order GetById(int id)
        {
            IFilmRepository filmRepository = new MySqlDbFilmRepository();
            string getOrderString = @"
                            SELECT o.idorder, o.`date`, o.iduser,
                                   SUM(IF(film_to_order.`type`='Покупка', films.purchase_price, films.rental_price))
                            FROM orders AS o
                            JOIN film_to_order ON film_to_order.idorder=o.idorder
                            JOIN films ON films.idfilm=film_to_order.idfilm
                            WHERE o.idorder=@idorder";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(getOrderString, connection);
                command.Parameters.AddWithValue("@idorder", id);

                using MySqlDataReader orderReader = command.ExecuteReader();

                if (!orderReader.Read())
                    return null;

                long idorder = orderReader.GetInt64(0);
                DateTime date = orderReader.GetDateTime(1);
                long iduser = orderReader.GetInt64(2);
                int price = orderReader.GetInt32(3);

                IEnumerable<FilmToOrder> films = filmRepository.GetByOrder(idorder);

                return new Order(id, iduser, date, price, films);

            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public IEnumerable<Order> GetByUser(long idUser)
        {
            List<Order> orders = new List<Order>();
            string commandString = @"
                        SELECT o.idorder, o.`date`, o.iduser,
                               COUNT(films.idfilm), 
                               SUM(IF(film_to_order.`type`= 'Покупка', films.purchase_price, films.rental_price))
                        FROM orders AS o
                        JOIN film_to_order ON film_to_order.idorder = o.idorder
                        JOIN films ON films.idfilm = film_to_order.idfilm
                        WHERE iduser = @iduser
                        GROUP BY o.idorder
                        ORDER BY o.`date` DESC";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand command = new MySqlCommand(commandString, connection);
                command.Parameters.AddWithValue("@iduser", idUser);

                using MySqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    long idorder = reader.GetInt64(0);
                    DateTime date = reader.GetDateTime(1);
                    long iduser = reader.GetInt64(2);
                    int amount = reader.GetInt32(3);
                    int price = reader.GetInt32(4);

                    orders.Add(new Order(idorder, iduser, date, price, amount));
                }
                
                return orders;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }
    }
}
