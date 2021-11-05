using System;

namespace ScsOnlineShop.Application.Model
{
    public class ShoppingCart
    {
        public ShoppingCart(Customer customer, Offer offer, int quantity, DateTime dateAdded)
        {
            CustomerId = customer.Id;
            Customer = customer;
            OfferId = offer.Id;
            Offer = offer;
            Quantity = quantity;
            DateAdded = dateAdded;
        }
        protected ShoppingCart() { }
        public int Id { get; private set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = default!;
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
