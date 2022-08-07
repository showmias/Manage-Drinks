using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ManageDrinks.Models
{
    [ExcludeFromCodeCoverage]
    public class Beer
    {
        [Key]
        public int BeerId { get; set; }
        public string? Name { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
    }
}