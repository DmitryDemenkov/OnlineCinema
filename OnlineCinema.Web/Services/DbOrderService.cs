using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Repositories;
using OnlineCinema.Web.RepositoryInterfaces;

namespace OnlineCinema.Web.Services
{
    public class DbOrderService
    {
        public DbOrderService()
        {
            orderRepository = new MySqlDbOrderRepository();
        }

        private IOrderRepository orderRepository;

        public Order GetOrder(int idorder, out int errorCode)
        {
            try
            {
                Order order = orderRepository.GetById(idorder);
                errorCode = 0;
                return order;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }
    }
}
