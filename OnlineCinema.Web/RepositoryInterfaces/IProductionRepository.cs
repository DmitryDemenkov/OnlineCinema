using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.RepositoryInterfaces
{
    interface IProductionRepository
    {
        IEnumerable<Production> GetByFilm(Film film);
    }
}
