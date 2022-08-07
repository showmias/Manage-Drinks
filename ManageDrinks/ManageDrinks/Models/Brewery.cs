using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ManageDrinks.Models
{
    [ExcludeFromCodeCoverage]
    public class Brewery
    {
        [Key]
        public int BreweryId { get; set; }
        public string? Name { get; set; }
    }
}