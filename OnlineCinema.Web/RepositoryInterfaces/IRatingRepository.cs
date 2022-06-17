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

        public Rating GetRating(int idfilm, long iduser);

        public Rating Append(Rating rating, int idfilm);

        public Rating Update(Rating rating, int idfilm);

        public void Delete(long iduser, int idfilm);
    }
}
