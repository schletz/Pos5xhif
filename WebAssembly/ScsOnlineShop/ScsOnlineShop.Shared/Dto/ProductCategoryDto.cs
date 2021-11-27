using System;
using System.Text.Json.Serialization;

namespace ScsOnlineShop.Shared.Dto
{
    public class ProductCategoryDto
    {
        public ProductCategoryDto(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
        }

        public Guid Guid { get; }
        public string Name { get; set; }
    }
}