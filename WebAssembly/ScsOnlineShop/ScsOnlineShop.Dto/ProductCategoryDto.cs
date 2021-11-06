using System;

namespace ScsOnlineShop.Dto
{
    public class ProductCategoryDto
    {
        public ProductCategoryDto(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
        }

        public string Name { get; set; }
        public Guid Guid { get; set; }
    }
}