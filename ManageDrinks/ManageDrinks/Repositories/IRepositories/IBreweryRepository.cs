using ManageDrinks.Models;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public interface IBreweryRepository : IRepository<Brewery>
    {
        Task<int> UpdateRecord(Brewery recordToUpdate);
        bool IsRecordExists(int id);
    }
}
