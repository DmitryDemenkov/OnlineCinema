using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Repositories;
using OnlineCinema.Web.RepositoryInterfaces;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.Services
{
    public class DbFilmService
    {
        public DbFilmService()
        {
            filmRepository = new MySqlDbFilmRepository();
        }

        private IFilmRepository filmRepository;

        public IEnumerable<Film> GetPopularFilms(out int errorCode)
        {
            try
            {
                IEnumerable<Film> films = filmRepository.GetPopular();
                errorCode = 0;
                return films;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }

        public Film GetFilm(int idfilm, out int errorCode)
        {
            try
            {
                Film film = filmRepository.GetById(idfilm);
                errorCode = 0;
                return film;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }
    }
}
