using Microsoft.EntityFrameworkCore;
using ScsOnlineShop.Application.Model;
using System;

namespace ScsOnlineShop.Application.Infrastructure
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions opt) : base(opt)
        {
        }

        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Offer> Offers => Set<Offer>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().Property(o => o.Price).HasPrecision(9, 4);
            modelBuilder.Entity<Offer>().Property(o => o.Price).HasPrecision(9, 4);
            modelBuilder.Entity<Customer>().OwnsOne(c => c.Address);
        }

        public void Seed()
        {
            var store = new Store(name: "Spengershop");
            Stores.Add(store);
            Stores.Add(new Store(name: "TGM Shop"));
            SaveChanges();

            var category = new ProductCategory(name: "Noten");
            ProductCategories.Add(category);
            var product = new Product(ean: 1001, "POS Note", productCategory: category);
            Products.Add(product);
            var offer = new Offer(product: product, store: store, 20000);
            Offers.Add(offer);
            var customer = new Customer(
                firstname: "Max",
                lastname: "Mustermann",
                email: "muster@spengergasse.at",
                address: new Address(Street: "Spengergasse 20", Zip: "1050", City: "Wien", Country: "AT"));
            Customers.Add(customer);

            var cartItem = new ShoppingCart(customer, offer, 2, new DateTime(2021, 10, 20));
            ShoppingCarts.Add(cartItem);
            SaveChanges();
        }
    }
}