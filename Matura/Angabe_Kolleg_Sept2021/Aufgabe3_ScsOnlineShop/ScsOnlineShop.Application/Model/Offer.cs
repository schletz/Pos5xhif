using System;

namespace ScsOnlineShop.Application.Model
{
    public class Offer
    {
        public Offer(Product product, Store store, decimal price)
        {
            ProductEan = product.Ean;
            Product = product;
            StoreId = store.Id;
            Store = store;
            Price = price;
            LastUpdate = DateTime.UtcNow;
        }

        protected Offer() { }
        public int Id { get; private set; }
        public int ProductEan { get; set; }  // FK: Property Name + Name des PK (EAN)
        public virtual Product Product { get; set; } = default!;
        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;
        // [Column(TypeName ="NUMERIC(9,4)")]  // Alter Stil; DB spezifische Typen werden modelliert.
        // In OnModelCreating mit .HasPrecision(9,4) zu definieren.
        public decimal Price { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
