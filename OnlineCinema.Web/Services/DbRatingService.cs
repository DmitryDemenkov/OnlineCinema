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

        public Rating GetRating(Film film, User user, out int errorCode)
        {
            try
            {
                Rating rating = RatingRepository.GetRating(film.Id, user.Id);
                errorCode = 0;
                return rating;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }

        public Rating AppendRating(int idfilm, Rating rating, out int errorCode)
        {
            try
            {
                Rating newRating = RatingRepository.Append(rating, idfilm);
                errorCode = 0;
                return rating;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }

        public Rating UpdateRating(int idfilm, Rating rating, out int errorCode)
        {
            try
            {
                Rating updatedRating = RatingRepository.Update(rating, idfilm);
                errorCode = 0;
                return rating;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }

        public void DeleteRating(long iduser, int idfilm, out int errorCode)
        {
            try
            {
                RatingRepository.Delete(iduser, idfilm);
                errorCode = 0;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
            }
        }
    }
}
