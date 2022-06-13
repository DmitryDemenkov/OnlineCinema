using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.Models
{
    public class Cart
    {
        public Cart (List<FilmToOrder> films = default)
        {
            if (films == default)
                Films = new List<FilmToOrder>();
            else
                Films = films;
        }

        public enum PurchaseType
        {
            PURCHASE,
            RENTAL
        }

        public List<FilmToOrder> Films { get; private set; }

        public int Length { get => Films.Count; }

        public int TotalCost { get => Films.Sum(x => x.Price); }

        public bool Contains (Film film) 
        {
            return Films.Exists(x => x.Film.Id == film.Id);
        }

        public void Add(Film film, PurchaseType type)
        {
            if (this.Contains(film))
                return;

            int price;
            string typeString;

            if (type == PurchaseType.PURCHASE)
            {
                price = film.PurchasePrice;
                typeString = "Покупка";
            }
            else
            {
                price = film.RentalPrice;
                typeString = "Аренда";
            }

            Films.Add(new FilmToOrder(film, typeString, price));
        }

        public void UpdateType(int idfilm, PurchaseType type)
        {
            if (!Films.Exists(x => x.Film.Id == idfilm))
                return;

            FilmToOrder film = Films.Find(x => x.Film.Id == idfilm);

            if (type == PurchaseType.PURCHASE)
            {
                film.Price = film.Film.PurchasePrice;
                film.Type = "Покупка";
            }
            else
            {
                film.Price = film.Film.RentalPrice;
                film.Type = "Аренда";
            }
        }

        public void Delete(int idfilm)
        {
            if (!Films.Exists(x => x.Film.Id == idfilm))
                return;

            Films.RemoveAll(x => x.Film.Id == idfilm);
        }
    }
}
