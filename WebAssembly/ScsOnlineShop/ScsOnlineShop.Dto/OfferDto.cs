using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Dto
{
    public class OfferDto
    {
        public OfferDto(ProductDto product, StoreDto store, decimal price, Guid guid)
        {
            Product = product;
            Store = store;
            Price = price;
            Guid = guid;
        }
        public Guid Guid { get; set; }
        public ProductDto Product { get; set; }
        public StoreDto Store { get; set; }
        public decimal Price { get; set; }
    }
}