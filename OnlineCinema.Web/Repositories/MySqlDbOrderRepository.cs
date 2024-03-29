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
        public Order Append(Cart cart, User user)
        {
            Order order = null;
            string appendOrderString = @"INSERT INTO orders (`date`, `iduser`)
                                     VALUES (@date, @iduser)";

            string appendFilmsString = @"INSERT INTO film_to_order (idfilm, idorder, `type`) VALUES ";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand appendOrderCommand = new MySqlCommand(appendOrderString, connection);
                DateTime dateTime = DateTime.Now;
                appendOrderCommand.Parameters.AddWithValue("@date", dateTime.ToString("yyyy-MM-dd HH-mm-ss"));
                appendOrderCommand.Parameters.AddWithValue("@iduser", user.Id);

                appendOrderCommand.ExecuteNonQuery();
                long idorder = appendOrderCommand.LastInsertedId;

                FilmToOrder lastFilm = cart.Films.Last();
                foreach(FilmToOrder film in cart.Films)
                {
                    appendFilmsString += $"('{film.Film.Id}', '{idorder}', '{film.Type}')";

                    if (film.Film.Id != lastFilm.Film.Id)
                        appendFilmsString += ", ";
                    else
                        appendFilmsString += ";";
                }

                using MySqlCommand appendFilmsCommand = new MySqlCommand(appendFilmsString, connection);
                appendFilmsCommand.ExecuteNonQuery();

                order = new Order(idorder, user.Id, dateTime, cart.TotalCost, cart.Films);
                return order;
            }
            catch (MySqlException exception)
            {
                throw new RepositoryException(exception.Number, exception.Message);
            }
        }

        public Order GetById(int id)
        {
            string getOrderString = @"
                            SELECT o.idorder, o.`date`, o.iduser,
                                   SUM(IF(film_to_order.`type`='Покупка', films.purchase_price, films.rental_price))
                            FROM orders AS o
                            JOIN film_to_order ON film_to_order.idorder=o.idorder
                            JOIN films ON films.idfilm=film_to_order.idfilm
                            WHERE o.idorder=@idorder";

            string getFilmsString = @"
                            SELECT f.idfilm, f.title, f.category, f.release_date, fto.`type`,
	                               IF(fto.`type`='Покупка', f.purchase_price, f.rental_price)
                            FROM films AS f
                            JOIN film_to_order AS fto ON fto.idfilm=f.idfilm
                            WHERE fto.idorder=@idorder";

            using MySqlConnection connection = MySqlDbUtil.GetConnection();
            connection.Open();

            try
            {
                using MySqlCommand getOrderCommand = new MySqlCommand(getOrderString, connection);
                getOrderCommand.Parameters.AddWithValue("@idorder", id);

                using MySqlDataReader orderReader = getOrderCommand.ExecuteReader();

                if (!orderReader.Read())
                    return null;

                long idorder = orderReader.GetInt64(0);
                DateTime date = orderReader.GetDateTime(1);
                long iduser = orderReader.GetInt64(2);
                int price = orderReader.GetInt32(3);

                orderReader.Close();

                List<FilmToOrder> films = new List<FilmToOrder>();

                using MySqlCommand getFilmsCommand = new MySqlCommand(getFilmsString, connection);
                getFilmsCommand.Parameters.AddWithValue("@idorder", idorder);

                using MySqlDataReader filmsReader = getFilmsCommand.ExecuteReader();

                while (filmsReader.Read())
                {
                    int idfilm = filmsReader.GetInt32(0);
                    string title = filmsReader.GetString(1);
                    string category = filmsReader.GetString(2);
                    DateTime releasedate = filmsReader.GetDateTime(3);
                    string type = filmsReader.GetString(4);
                    int filmPrice = filmsReader.GetInt32(5);

                    Film film = new Film(idfilm, title, category, releasedate);
                    films.Add(new FilmToOrder(film, type, filmPrice));
                }

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
