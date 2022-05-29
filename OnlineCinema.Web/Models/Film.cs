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
                            int purchasePrise, int rentalPrice, int rentalDuration)
        {
            Id = id;
            Title = title;
            Category = category;
            Annotation = annotation;
            ReleaseDate = releaseDate;
            PurchasePrice = purchasePrise;
            RentalPrice = rentalPrice;
            RentalDuration = rentalDuration;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Category { get; private set; }

        public string Annotation { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public int PurchasePrice { get; private set; }

        public int RentalPrice { get; private set; }

        public int RentalDuration { get; private set; }
    }
}
