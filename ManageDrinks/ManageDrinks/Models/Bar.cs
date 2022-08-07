using System.Diagnostics.CodeAnalysis;

namespace ManageDrinks.Models
{
    [ExcludeFromCodeCoverage]
    public class Bar
    {
        public int BarId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}