using Microsoft.EntityFrameworkCore;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Application.Services
{
    public class OrderService
    {
        private readonly ShopContext _db;

        public OrderService(ShopContext db)
        {
            _db = db;
        }
        public bool AddToShoppingCart(int customerId, int offerId, int qantity)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer is null) { return false; }
            var offer = _db.Offers.FirstOrDefault(o => o.Id == offerId);
            if (offer is null) { return false; }
            var existingItem = _db.ShoppingCarts.FirstOrDefault(s => s.OfferId == offerId);
            if (existingItem is not null)
            {
                existingItem.Quantity += qantity;
                existingItem.DateAdded = DateTime.UtcNow;
                _db.SaveChanges();
                return true;
            }
            var sc = new ShoppingCart(
                customer: customer,
                offer: offer,
                quantity: qantity,
                dateAdded: DateTime.UtcNow);
            _db.ShoppingCarts.Add(sc);
            _db.SaveChanges();
            return true;
        }
        public void RemoveFromShoppingCart(int customerId, int offerId)
        {
            var item = _db.ShoppingCarts.FirstOrDefault(
                s => s.CustomerId == customerId && s.OfferId == offerId);
            if (item is null) { return; }
            _db.ShoppingCarts.Remove(item);
            _db.SaveChanges();
        }

        public bool Checkout(int customerId)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer is null) { return false; }

            var items = _db.ShoppingCarts.Where(s => s.CustomerId == customerId)
                .Include(s => s.Offer) // JOIN, da wir nach dem Offer.StoreId gruppieren.
                .ToList();
            if (!items.Any()) { return false; }
            var stores = _db.Stores.ToDictionary(s => s.Id, s => s);

            foreach (var storeItems in items.GroupBy(i => i.Offer.StoreId))
            {
                var order = new Order(
                    orderDate: DateTime.UtcNow,
                    store: stores[storeItems.Key],
                    customer: customer);
                _db.Orders.Add(order);
                // Iteriere über alle Elemente der Gruppe
                foreach (var item in storeItems)
                {
                    var orderItem = new OrderItem(
                        quantity: item.Quantity,
                        product: item.Offer.Product,
                        price: item.Offer.Price,
                        order: order);
                    _db.OrderItems.Add(orderItem);
                }
            }
            _db.SaveChanges();
            return true;
        }
    }
}
