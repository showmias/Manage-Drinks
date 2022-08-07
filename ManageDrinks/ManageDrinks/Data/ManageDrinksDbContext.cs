
using ManageDrinks.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageDrinks.Data
{
    public class ManageDrinksDbContext : DbContext
    {
        public ManageDrinksDbContext() : base()
        {
            Database.EnsureCreated();
        }
        public ManageDrinksDbContext(DbContextOptions<ManageDrinksDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<BreweryBeers> BreweryBeers { get; set; }
        public DbSet<BarBeers> BarBeers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>().ToTable("Beer");
            modelBuilder.Entity<Brewery>().ToTable("Brewery");
            modelBuilder.Entity<Bar>().ToTable("Bar");
            modelBuilder.Entity<BreweryBeers>().ToTable("BreweryBeers");
            modelBuilder.Entity<BarBeers>().ToTable("BarBeers");

        }
    }
}