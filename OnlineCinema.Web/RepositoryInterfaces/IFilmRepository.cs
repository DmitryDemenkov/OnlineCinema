using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.RepositoryInterfaces
{
    public interface IFilmRepository : IRepository<Film>
    {
        IEnumerable<Film> GetPopular();

        IEnumerable<FilmToLibrary> GetByUser(long iduser);
    }
}
