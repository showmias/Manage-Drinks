using ManageDrinks.Data;
using ManageDrinks.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageDrinks.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly ManageDrinksDbContext dbContext;

        public BreweryRepository(ManageDrinksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        async Task<IEnumerable<Brewery>> IRepository<Brewery>.GetAll()
        {
            return await Task.FromResult(dbContext.Breweries);
        }

        async Task<Brewery> IRepository<Brewery>.GetById(int id)
        {
            return await dbContext.Breweries.FindAsync(id);
        }

        public async Task<int> InsertRecord(Brewery brewery)
        {
            dbContext.Breweries.Add(brewery);
            return await Task.FromResult(dbContext.SaveChanges());

        }

        public async Task<int> UpdateRecord(Brewery brewery)
        {
            dbContext.Entry(brewery).State = EntityState.Modified;
            return await Task.FromResult(dbContext.SaveChanges());

        }

        public bool IsRecordExists(int id)
        {
          return dbContext.Breweries.Any(e => e.BreweryId == id);
        }
    }
}
