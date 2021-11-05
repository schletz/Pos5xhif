using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScsOnlineShop.Application.Model
{
    public class Product
    {
        public Product(int ean, string name, ProductCategory productCategory)
        {
            Ean = ean;
            Name = name;
            ProductCategory = productCategory;
            ProductCategoryId = productCategory.Id;
        }
        protected Product() { }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ean { get; private set; }
        public string Name { get; set; } = default!;
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; } = default!;
    }
}
