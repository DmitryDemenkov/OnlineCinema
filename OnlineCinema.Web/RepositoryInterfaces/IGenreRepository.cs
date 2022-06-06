using OnlineCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web.RepositoryInterfaces
{
    interface IGenreRepository
    {
        public IEnumerable<Genre> GetByFilm(int idfilm);
    }
}
