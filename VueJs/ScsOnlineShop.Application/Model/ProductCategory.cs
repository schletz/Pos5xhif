using System;
using System.Collections.Generic;

namespace ScsOnlineShop.Application.Model
{
    public class ProductCategory
    {
        public ProductCategory(string name)
        {
            Name = name;
            Guid = Guid.NewGuid();
        }

        protected ProductCategory()
        {
        }

        public int Id { get; private set; }
        public string Name { get; set; } = default!;
        public Guid Guid { get; set; }
        public virtual ICollection<Product> Products { get; } = new List<Product>(0);
    }
}