using ScsOnlineShop.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScsOnlineShop.Test
{
    [Collection("Sequential")]
    public class OrderServiceTests
    {
        [Fact]
        public void AddToShoppingCartNewOrderSuccessTest()
        {
            using var db = TestHelpers.GetShopContext();
            var service = new OrderService(db);

            var customer = db.Customers.First(c =>
                c.Email == "muster@spengergasse.at");
            var offer = db.Offers.First(o => o.ProductEan == 1002);
            var result = service.AddToShoppingCart(customer.Id,
                offer.Id, 1);
            Assert.True(result);
            // Ich habe ein Order in den Musterdaten, daher
            // muss ich jetzt 2 haben.
            Assert.True(db.ShoppingCarts.Count() == 2);
        }


        /// <summary>
        /// Testet, ob ein existierendes Order im Warenkorb korrekt
        /// behandelt wird (Anzahl wird erhöht und DateAdded wird aktualisiert)
        /// </summary>
        [Fact]
        public void AddToShoppingCartExistingOrderSuccessTest()
        {
            using var db = TestHelpers.GetShopContext();
            var service = new OrderService(db);

            var customer = db.Customers.First(c =>
                c.Email == "muster@spengergasse.at");
            // POS Note, denn diese liegt in den Musterdaten schon im
            // Shopping Cart des Customers.
            var offer = db.Offers.First(o => o.ProductEan == 1001);

            var result = service.AddToShoppingCart(customer.Id,
                offer.Id, 1);
            Assert.True(result);
            Assert.True(db.ShoppingCarts.Count() == 1);
        }

        [Fact]
        public void AddToShoppingReturnsFalseOnInvalidCustomerId()
        {
            using var db = TestHelpers.GetShopContext();
            var service = new OrderService(db);

            var customer = db.Customers.First(c =>
                c.Email == "muster@spengergasse.at");
            // POS Note, denn diese liegt in den Musterdaten schon im
            // Shopping Cart des Customers.
            var offer = db.Offers.First(o => o.ProductEan == 1001);

            var result = service.AddToShoppingCart(-1, offer.Id, 1);
            Assert.False(result);
        }
        [Fact]
        public void AddToShoppingReturnsFalseOnInvalidOfferId()
        {
            using var db = TestHelpers.GetShopContext();
            var service = new OrderService(db);

            var customer = db.Customers.First(c =>
                c.Email == "muster@spengergasse.at");
            // POS Note, denn diese liegt in den Musterdaten schon im
            // Shopping Cart des Customers.
            var offer = db.Offers.First(o => o.ProductEan == 1001);

            var result = service.AddToShoppingCart(customer.Id, -1, 1);
            Assert.False(result);
        }

        [Fact]
        public void RemoveFromShoppingCartSuccessTest()
        {
            using var db = TestHelpers.GetShopContext();
            var service = new OrderService(db);

            var item = db.ShoppingCarts.First();
            service.RemoveFromShoppingCart(item.CustomerId, item.OfferId);
            Assert.True(db.ShoppingCarts.FirstOrDefault(s =>
                s.CustomerId == item.CustomerId && s.OfferId == item.OfferId) is null);
        }

        [Fact]
        public void CheckoutSuccessTest()
        {
            using var db = TestHelpers.GetShopContext();
            var service = new OrderService(db);

            var customer = db.Customers.First();
            service.Checkout(customer.Id);
            Assert.True(db.Orders.Any(o => o.CustomerId == customer.Id));

        }
    }
}
