using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Range(1000, 9999, ErrorMessage = "Die EAN muss 4stellig sein.")]
        public int Ean { get; set; }
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Der Name muss zwischen 2 und 255 Stellen lang sein.")]
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public List<Offer> Offers { get; } = new();
    }
}
