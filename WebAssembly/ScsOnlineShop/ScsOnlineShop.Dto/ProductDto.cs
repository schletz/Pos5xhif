using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScsOnlineShop.Dto
{
    public class ProductDtoBase
    {
        public ProductDtoBase(int ean, string name, Guid productCategoryId)
        {
            Ean = ean;
            Name = name;
            ProductCategoryGuid = productCategoryId;
        }

        [Display(Name = "EAN", Prompt = "6stellige EAN Nummer")]
        [Range(100000, 999999, ErrorMessage = "Die EAN muss 6 Stellen haben.")]
        public int Ean { get; private set; }

        [Required]
        [RegularExpression(@"^[A-ZÄÖÜ]", ErrorMessage = "Der Produktname muss mit einem Buchstaben beginnen.")]
        public string Name { get; set; }

        public Guid ProductCategoryGuid { get; set; }
    }

    public class ProductDto : ProductDtoBase
    {
        public ProductDto(int ean, string name, ProductCategoryDto productCategory) :
            base(ean, name, productCategory.Guid)
        {
            ProductCategory = productCategory;
        }

        public ProductCategoryDto ProductCategory { get; }
    }
}