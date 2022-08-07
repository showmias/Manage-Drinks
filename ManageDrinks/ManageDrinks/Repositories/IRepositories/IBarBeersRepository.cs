using ManageDrinks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public interface IBarBeersRepository : IRepository<BarBeers>
    {
        Task<IEnumerable<BarBeers>> GetBarBeersById(int id);
    }
}
