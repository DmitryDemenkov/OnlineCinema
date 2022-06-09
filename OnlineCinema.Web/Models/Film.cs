using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web.Models
{
    public class Film
    {
        public Film(int id, string title, string category, DateTime releaseDate)
        {
            Id = id;
            Title = title;
            Category = category;
            ReleaseDate = releaseDate;
        }

        public Film(int id, string title, string category, string annotation, DateTime releaseDate,
                            int purchasePrise, int rentalPrice, int rentalDuration, string ageRestriction,
                            float middleRating, int ratingAmount)
        {
            Id = id;
            Title = title;
            Category = category;
            Annotation = annotation;
            ReleaseDate = releaseDate;
            PurchasePrice = purchasePrise;
            RentalPrice = rentalPrice;
            RentalDuration = rentalDuration;
            AgeRestriction = ageRestriction;
            MiddleRating = middleRating;
            RatingAmount = ratingAmount;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Category { get; private set; }

        public string Annotation { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public int PurchasePrice { get; private set; }

        public int RentalPrice { get; private set; }

        public int RentalDuration { get; private set; }

        public string AgeRestriction { get; private set; }

        public float MiddleRating { get; private set; }

        public int RatingAmount { get; private set; }
    }

    public struct FilmToLibrary
    {
        public FilmToLibrary(Film film, string type, int timeLeft)
        {
            Film = film;
            Type = type;
            TimeLeft = timeLeft;
        }

        public Film Film { get; private set; }

        public string Type { get; private set; }

        public int TimeLeft { get; private set;  }
    }

    public struct FilmToOrder
    {
        public FilmToOrder(Film film, string type, int price)
        {
            Film = film;
            Type = type;
            Price = price;
        }

        public Film Film { get; private set; }

        public string Type { get; private set; }

        public int Price { get; private set; }
    }
}
