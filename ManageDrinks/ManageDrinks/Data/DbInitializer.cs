using System.Diagnostics.CodeAnalysis;

namespace ManageDrinks.Data
{
    public static class DbInitializer
    {
        [ExcludeFromCodeCoverage]
        public static void Initialize(ManageDrinksDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}