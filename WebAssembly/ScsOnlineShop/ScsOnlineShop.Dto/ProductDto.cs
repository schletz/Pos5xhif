using System.ComponentModel.DataAnnotations;

namespace ScsOnlineShop.Dto
{
    public class ProductDto
    {
        public ProductDto(int ean, string name)
        {
            Ean = ean;
            Name = name;
        }

        [Display(Name = "EAN", Prompt = "6stellige EAN Nummer")]
        [Range(100000, 999999, ErrorMessage = "Die EAN muss 6 Stellen haben.")]
        public int Ean { get; private set; }

        [Required]
        [RegularExpression(@"^[A-ZÄÖÜ]", ErrorMessage = "Der Produktname muss mit einem Buchstaben beginnen.")]
        public string Name { get; set; }
    }
}