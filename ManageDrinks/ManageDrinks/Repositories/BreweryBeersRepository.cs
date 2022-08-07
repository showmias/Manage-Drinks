using ManageDrinks.Data;
using ManageDrinks.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public class BreweryBeersRepository : IBreweryBeersRepository
    {
        private readonly ManageDrinksDbContext dbContext;

        public BreweryBeersRepository(ManageDrinksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        async Task<IEnumerable<BreweryBeers>> IRepository<BreweryBeers>.GetAll()
        {
            var breweryBeers = await dbContext.BreweryBeers
                            .Include(e => e.Beer)
                            .Include(e => e.Brewery).ToListAsync();

            return await Task.FromResult(breweryBeers);
        }

        async Task<BreweryBeers> IRepository<BreweryBeers>.GetById(int id)
        {
            var breweryBeers = await dbContext.BreweryBeers.Where(x => x.BreweryId == id)
              .Include(e => e.Beer)
                          .Include(e => e.Brewery).FirstOrDefaultAsync();

            return await Task.FromResult(breweryBeers);
        }

        public async Task<IEnumerable<BreweryBeers>> GetBreweryBeersById(int id)
        {
            var breweryBeers = await dbContext.BreweryBeers.Where(x => x.BreweryId == id)
                            .Include(e => e.Beer)
                            .Include(e => e.Brewery).ToListAsync();

            return await Task.FromResult(breweryBeers);
        }

        public async Task<int> InsertRecord(BreweryBeers breweryBeers)
        {
            dbContext.BreweryBeers.Add(breweryBeers);
            return await Task.FromResult(dbContext.SaveChanges());

        }
    }
}
