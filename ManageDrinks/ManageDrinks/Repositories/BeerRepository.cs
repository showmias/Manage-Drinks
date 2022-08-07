using ManageDrinks.Data;
using ManageDrinks.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly ManageDrinksDbContext dbContext;

        public BeerRepository(ManageDrinksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        async Task<IEnumerable<Beer>> IRepository<Beer>.GetAll()
        {
            return await Task.FromResult(dbContext.Beers);
        }

        async Task<Beer> IRepository<Beer>.GetById(int id)
        {
            return await dbContext.Beers.FindAsync(id);
        }

        public async Task<IEnumerable<Beer>> GetBeerByVolume(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            return await Task.FromResult(dbContext.Beers.Where(x => x.PercentageAlcoholByVolume >= gtAlcoholByVolume
                                                         && x.PercentageAlcoholByVolume <= ltAlcoholByVolume));
        }
        public async Task<int> InsertRecord(Beer beer)
        {
            dbContext.Beers.Add(beer);
            return await Task.FromResult(dbContext.SaveChanges());

        }

        public async Task<int> UpdateRecord(Beer beer)
        {
            dbContext.Entry(beer).State = EntityState.Modified;
            return await Task.FromResult(dbContext.SaveChanges());

        }
        public bool IsRecordExists(int id)
        {
            return dbContext.Beers.Any(e => e.BeerId == id);
        }
    }
}
