using Bogus;
using Microsoft.EntityFrameworkCore;
using ScsOnlineShop.Application.Model;
using System;
using System.Linq;

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
            Randomizer.Seed = new Random(1318);

            var productCategories = new Faker<ProductCategory>("de")
                .CustomInstantiator(f => new ProductCategory(f.Commerce.ProductAdjective()))
                .Generate(3)
                .GroupBy(p => p.Name).Select(g => g.First())
                .ToList();
            ProductCategories.AddRange(productCategories);
            SaveChanges();

            var products = new Faker<Product>("de").CustomInstantiator(f =>
                    new Product(
                        f.Random.Int(100000, 999999),
                        f.Commerce.Product(),
                        f.Random.ListItem(productCategories)))
                .Generate(24)
                .GroupBy(p => p.Name).Select(g => g.First())
                .ToList();
            Products.AddRange(products);
            SaveChanges();

            var stores = new Faker<Store>("de")
                .CustomInstantiator(f => new Store(name: f.Company.CompanyName()))
                .Generate(8)
                .GroupBy(s => s.Name).Select(g => g.First())
                .ToList();

            Stores.AddRange(stores);
            SaveChanges();

            var offers = new Faker<Offer>("de")
                .CustomInstantiator(f => new Offer(f.Random.ListItem(products), f.Random.ListItem(stores), Math.Round(f.Random.Decimal(100, 999), 2)))
                .Generate(80);
            Offers.AddRange(offers);
            SaveChanges();

            var customers = new Faker<Customer>("de")
                .CustomInstantiator(f => new Customer(
                    firstname: f.Name.FirstName(),
                    lastname: f.Name.LastName(),
                    email: f.Internet.Email(),
                    address: new Address(f.Address.StreetAddress(), f.Random.Int(1000, 9999).ToString(), f.Address.City(), f.Address.Country())))
                .Generate(8);
            Customers.AddRange(customers);
            SaveChanges();
        }
    }
}