using ManageDrinks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public interface IBeerRepository : IRepository<Beer>
    {
        Task<IEnumerable<Beer>> GetBeerByVolume(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
        Task<int> UpdateRecord(Beer recordToUpdate);
        bool IsRecordExists(int id);
    }
}
