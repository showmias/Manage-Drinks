using ManageDrinks.Data;
using ManageDrinks.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public class BarBeersRepository : IBarBeersRepository
    {
        private readonly ManageDrinksDbContext dbContext;

        public BarBeersRepository(ManageDrinksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        async Task<IEnumerable<BarBeers>> IRepository<BarBeers>.GetAll()
        {
            var barBeers = await dbContext.BarBeers
                            .Include(e => e.Beer)
                            .Include(e => e.Bar).ToListAsync();

            return await Task.FromResult(barBeers);
        }

        async Task<BarBeers> IRepository<BarBeers>.GetById(int id)
        {
            var barBeers = await dbContext.BarBeers.Where(x => x.BarId == id)
              .Include(e => e.Beer)
                          .Include(e => e.Bar).FirstOrDefaultAsync();

            return await Task.FromResult(barBeers);
        }

        public async Task<IEnumerable<BarBeers>> GetBarBeersById(int id)
        {
            var barBeers = await dbContext.BarBeers.Where(x => x.BarId == id)
                            .Include(e => e.Beer)
                            .Include(e => e.Bar).ToListAsync();

            return await Task.FromResult(barBeers);
        }

        public async Task<int> InsertRecord(BarBeers barBeers)
        {
            dbContext.BarBeers.Add(barBeers);
            return await Task.FromResult(dbContext.SaveChanges());

        }
    }
}
