using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.RepositoryInterfaces
{
    interface IRatingRepository
    {
        public IEnumerable<Rating> GetByFilm(int idfilm, long iduser = 0);

        public IEnumerable<Rating> GetByUser(long iduser);
    }
}
