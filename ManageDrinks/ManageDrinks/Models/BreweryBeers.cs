using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageDrinks.Models
{
    public class BreweryBeers
    {
        [Key]
        public int BreweryBeerId { get; set; }

        //[ForeignKey("BreweryId")]
        public int BreweryId { get; set; }

        //[ForeignKey("BeerId")]
        public int BeerId { get; set; }

        [ForeignKey("BeerId")]
        public Beer Beer { get; set; }

        [ForeignKey("BreweryId")]
        public Brewery Brewery { get; set; }
    }
}
