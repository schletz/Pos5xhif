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

        public bool Checkout(int customerId)
        {
            var customer = _db.Customers
                .FirstOrDefault(c => c.Id == customerId);

            if (customer is null) { return false; }
            if (!customer.ShoppingCarts.Any()) { return false; }
            var stores = _db.Stores.ToDictionary(s => s.Id, s => s);

            foreach (var g in customer.ShoppingCarts.GroupBy(s => s.Offer.StoreId))
            {
                var order = new Order(DateTime.Now, stores[g.Key], customer);
                _db.Orders.Add(order);
                _db.SaveChanges();
                foreach (var item in g)
                {
                    var orderItem = new OrderItem(
                        item.Quantity, item.Offer.Product,
                        item.Offer.Price, order);
                    _db.OrderItems.Add(orderItem);
                }
            }
            _db.SaveChanges();
            return true;
        }
    }
}
