using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.RepositoryInterfaces
{
    interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetByUser(long idUser);

        Order Append(Cart cart, User user);
    }
}
