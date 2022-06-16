using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.RepositoryInterfaces;
using OnlineCinema.Web.Repositories;

namespace OnlineCinema.Web.Services
{
    public class DbRatingService
    {
        public DbRatingService()
        {
            RatingRepository = new MySqlDbRatingRepository();
        }

        private IRatingRepository RatingRepository;

        public IEnumerable<Rating> GetByFilm(Film film, out int errorCode, User user = null)
        {
            long iduser = 0;
            if (user != null)
                iduser = user.Id;

            try
            {
                IEnumerable<Rating> ratings = RatingRepository.GetByFilm(film.Id, iduser);
                errorCode = 0;
                return ratings;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }
    }
}
