using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using System.Linq;
using Bogus.Extensions;

namespace SPG_Fachtheorie.Aufgabe2.Infrastructure
{

    /// <summary>
    /// Datenbankkontext für die Aufgabe 2
    /// HIER SIND KEINE EDITIERUNGEN VORZUNEHMEN!
    /// </summary>
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options) { }
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Offer> Offers => Set<Offer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<Store> Stores => Set<Store>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().OwnsOne(c => c.Address);
        }

        public void Seed()
        {
            var faker = new Faker();
            Randomizer.Seed = new Random(840);

            var customers = new Faker<Customer>("de").Rules((f, c) =>
            {
                c.Firstname = f.Name.FirstName();
                c.Lastname = f.Name.LastName();
                c.Email = f.Internet.Email();
                c.Address = new Address
                {
                    Street = f.Address.StreetAddress(),
                    Zip = f.Random.Int(1000, 9999).ToString(),
                    City = f.Address.City(),
                    Country = f.Address.CountryCode()
                };
            })
            .Generate(10)
            .ToList();
            Customers.AddRange(customers);
            SaveChanges();

            var stores = new Faker<Store>("de").Rules((f, s) =>
            {
                s.Name = f.Company.CompanyName();
            })
            .Generate(10)
            .GroupBy(s => s.Name).Select(g => g.First())
            .Take(5)
            .ToList();
            Stores.AddRange(stores);
            SaveChanges();

            var productCategories = faker.Commerce.Categories(5)
                .Select(c => new ProductCategory { Name = c })
                .ToList();
            ProductCategories.AddRange(productCategories);
            SaveChanges();

            var productCategoriesWithProducs = productCategories.Take(3).ToList();
            var products = new Faker<Product>("de").Rules((f, p) =>
            {
                p.Ean = f.Random.Int(100000, 999999);
                p.Name = f.Commerce.ProductName();
                p.ProductCategory = f.Random.ListItem(productCategoriesWithProducs);
            })
            .Generate(25)
            .ToList();
            Products.AddRange(products);
            SaveChanges();

            var productsWithOffers = products.Take(20).ToList();
            var offers = new Faker<Offer>("de").Rules((f, o) =>
            {
                o.Product = f.Random.ListItem(productsWithOffers);
                o.Store = f.Random.ListItem(stores);
                o.Price = Math.Round(f.Random.Decimal(0, 9999), 2);
                o.LastUpdate = new DateTime(2020, 1, 1).AddSeconds(f.Random.Int(0, 366 * 86400));
            })
            .Generate(40)
            .ToList();
            Offers.AddRange(offers);
            SaveChanges();

            var customersWithShoppingCard = customers.Take(5).ToList();
            var shoppingCarts = new Faker<ShoppingCart>("de").Rules((f, s) =>
            {
                s.Customer = f.Random.ListItem(customersWithShoppingCard);
                s.DateAdded = new DateTime(2020, 1, 1).AddSeconds(f.Random.Int(0, 366 * 86400));
                s.Offer = f.Random.ListItem(offers);
                s.Quantity = f.Random.Int(1, 4);
            })
            .Generate(20)
            .ToList();
            ShoppingCarts.AddRange(shoppingCarts);
            SaveChanges();

        }
    }
}
