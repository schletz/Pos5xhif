using Microsoft.EntityFrameworkCore;
using ScsOnlineShop.Application.Model;

namespace ScsOnlineShop.Application.Infrastructure
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions opt) : base(opt) { }

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
    }
}