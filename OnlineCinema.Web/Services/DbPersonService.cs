using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.RepositoryInterfaces;
using OnlineCinema.Web.Repositories;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.Services
{
    public class DbPersonService
    {
        public DbPersonService()
        {
            productionRepository = new MySqlDbProductionRepository();
        }

        private IProductionRepository productionRepository;

        public IEnumerable<Production> GetProductions(Person person, out int errorCode)
        {
            try
            {
                IEnumerable<Production> productions = productionRepository.GetByPerson(person);
                errorCode = 0;
                return productions;
            }
            catch (RepositoryException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }
    }
}
