using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web.RepositoryInterfaces
{
    public interface IRepository<T>
    {
        T GetById();
    }
}
