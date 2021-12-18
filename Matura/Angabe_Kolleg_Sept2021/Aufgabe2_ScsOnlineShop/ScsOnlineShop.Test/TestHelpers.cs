using Microsoft.EntityFrameworkCore;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Application.Model;
using System;

namespace ScsOnlineShop.Test
{
    public static class TestHelpers
    {
        private static DbContextOptions _opt = new DbContextOptionsBuilder()
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=Shop.db")
                .Options;
        public static ShopContext GetShopContext()
        {
            using (var db = new ShopContext(_opt))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var store = new Store(name: "Spengershop");
                db.Stores.Add(store);
                var category = new ProductCategory(name: "Noten");
                db.ProductCategories.Add(category);
                var product = new Product(ean: 1001, "POS Note", productCategory: category);
                db.Products.Add(product);
                var offer = new Offer(product: product, store: store, 20000);
                db.Offers.Add(offer);

                var productDbi = new Product(ean: 1002, "DBI Note", productCategory: category);
                db.Products.Add(productDbi);
                var offerDbi = new Offer(product: productDbi, store: store, 15000);
                db.Offers.Add(offerDbi);
                
                var customer = new Customer(
                    firstname: "Max",
                    lastname: "Mustermann",
                    email: "muster@spengergasse.at",
                    address: new Address(Street: "Spengergasse 20", Zip: "1050", City: "Wien", Country: "AT"));
                db.Customers.Add(customer);

                var cartItem = new ShoppingCart(customer, offer, 2, new DateTime(2021, 10, 20));
                db.ShoppingCarts.Add(cartItem);
                db.SaveChanges();
            }
            return new ShopContext(_opt);
        }
    }
}
