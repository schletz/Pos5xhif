using System.Collections.Generic;

namespace ScsOnlineShop.Application.Model
{
    public class ProductCategory
    {
        public int Id { get; private set; }
        public string Name { get; set; } = default!;
        public virtual ICollection<Product> Products { get; } = new List<Product>(0);

        public ProductCategory(string name)
        {
            Name = name;
        }
        protected ProductCategory() { }
    }
}
