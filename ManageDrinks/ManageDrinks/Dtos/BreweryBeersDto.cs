using ManageDrinks.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ManageDrinks.Dtos
{
    [ExcludeFromCodeCoverage]
    public class BreweryBeersDto
    {
        public int BreweryId { get; set; }
        public List<Beer> Beers { get; set; }
    }
}
