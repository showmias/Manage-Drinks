using ManageDrinks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public interface IBreweryBeersRepository : IRepository<BreweryBeers>
    {
        Task<IEnumerable<BreweryBeers>> GetBreweryBeersById(int id);

    }
}
