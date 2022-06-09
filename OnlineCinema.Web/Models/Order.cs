using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web.Models
{
    public class Order
    {
        public Order(long id, long idUser, DateTime date, int totalCost, int filmsAmount)
        {
            Id = id;
            IdUser = idUser;
            Date = date;
            TotalCost = totalCost;
            FilmsAmount = filmsAmount;
        }

        public Order(long id, long idUser, DateTime date, int totalCost, IEnumerable<FilmToOrder> films)
        {
            Id = id;
            IdUser = idUser;
            Date = date;
            TotalCost = totalCost;
            Films = films;
        }

        public long Id { get; private set; }

        public long IdUser { get; private set; }

        public DateTime Date { get; private set; }

        public int FilmsAmount { get; private set; }

        public int TotalCost { get; private set; }

        public IEnumerable<FilmToOrder> Films { get; private set; }
    }
}
