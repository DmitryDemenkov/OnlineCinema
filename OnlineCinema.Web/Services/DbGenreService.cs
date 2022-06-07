using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Repositories;
using OnlineCinema.Web.RepositoryInterfaces;

namespace OnlineCinema.Web.Services
{
    public class DbGenreService
    {
        public DbGenreService()
        {
            filmRepository = new MySqlDbFilmRepository();
        }

        private IFilmRepository filmRepository;

        public IEnumerable<Film> GetFilms(int idgenre, out int errorCode)
        {
            try
            {
                IEnumerable<Film> films = filmRepository.GetByGenre(idgenre);
                errorCode = 0;
                return films;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }
    }
}
