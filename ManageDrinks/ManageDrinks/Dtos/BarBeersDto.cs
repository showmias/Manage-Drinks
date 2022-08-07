using ManageDrinks.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ManageDrinks.Dtos
{
    [ExcludeFromCodeCoverage]
    public class BarBeersDto
    {
        public int BarId { get; set; }
        public List<Beer> Beers { get; set; }
    }
}
