using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ManageDrinks.Models
{
    [ExcludeFromCodeCoverage]
    public class BarBeers
    {
        [Key]
        public int BarBeerId { get; set; }

        public int BarId { get; set; }

        public int BeerId { get; set; }

        [ForeignKey("BeerId")]
        public Beer Beer { get; set; }

        [ForeignKey("BarId")]
        public Bar Bar { get; set; }
    }
}
