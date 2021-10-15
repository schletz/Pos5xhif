using Microsoft.EntityFrameworkCore;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Application.Model;

namespace ScsOnlineShop.Test
{
    public static class TestHelpers
    {
        private static DbContextOptions _opt = new DbContextOptionsBuilder()
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
                db.Offers.Add(new Offer(product: product, store: store, 20000));
                var customer = new Customer(
                    firstname: "Max",
                    lastname: "Mustermann",
                    email: "muster@spengergasse.at",
                    address: new Address(Street: "Spengergasse 20", Zip: "1050", City: "Wien", Country: "AT"));
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            return new ShopContext(_opt);
        }
    }
}
