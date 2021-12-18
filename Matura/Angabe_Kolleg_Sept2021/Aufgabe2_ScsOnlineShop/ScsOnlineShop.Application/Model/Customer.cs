using System;
using System.Collections.Generic;
using System.Linq;

namespace ScsOnlineShop.Application.Model
{
    public class Customer
    {
        public Customer(string firstname, string lastname, string email, Address address)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Address = address;
        }
        protected Customer() { }   // EF Core Proxy
        public int Id { get; private set; }
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Address Address { get; set; } = default!;
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; } = new List<ShoppingCart>(0);

        public bool AddToShoppingCart(Offer offer, int quantity)
        {
            var shoppingCart = new ShoppingCart(
                customer: this,
                offer: offer,
                quantity: quantity,
                dateAdded: DateTime.UtcNow);
            ShoppingCarts.Add(shoppingCart);
            return true;
        }
    }
}
