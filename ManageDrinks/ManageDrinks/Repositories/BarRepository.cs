using ManageDrinks.Data;
using ManageDrinks.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public class BarRepository : IBarRepository
    {
        private readonly ManageDrinksDbContext dbContext;

        public BarRepository(ManageDrinksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        async Task<IEnumerable<Bar>> IRepository<Bar>.GetAll()
        {
            return await Task.FromResult(dbContext.Bars);
        }

        async Task<Bar> IRepository<Bar>.GetById(int id)
        {
            return await dbContext.Bars.FindAsync(id);
        }

        public async Task<int> InsertRecord(Bar bar)
        {
            dbContext.Bars.Add(bar);
            return await Task.FromResult(dbContext.SaveChanges());

        }

        public async Task<int> UpdateRecord(Bar bar)
        {
            dbContext.Entry(bar).State = EntityState.Modified;
            return await Task.FromResult(dbContext.SaveChanges());

        }

        public bool IsRecordExists(int id)
        {
            return dbContext.Bars.Any(e => e.BarId == id);
        }
    }
}
