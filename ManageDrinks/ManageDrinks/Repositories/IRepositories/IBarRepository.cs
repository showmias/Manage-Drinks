using ManageDrinks.Models;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public interface IBarRepository : IRepository<Bar>
    {
        Task<int> UpdateRecord(Bar recordToUpdate);
        bool IsRecordExists(int id);

    }
}
