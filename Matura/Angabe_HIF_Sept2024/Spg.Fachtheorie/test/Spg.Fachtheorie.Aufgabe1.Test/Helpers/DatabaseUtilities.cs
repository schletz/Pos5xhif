using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.Fachtheorie.Aufgabe1.Infrastructure;
using System.Net.Mail;

namespace Spg.Fachtheorie.Aufgabe1.Test.Helpers
{
    public class DatabaseUtilities
    {
        public static Aufgabe1Database GetDbFile()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source = C:\\Scratch\\Aufgabe1_Tests.db")
                .Options;

            Aufgabe1Database db = new Aufgabe1Database(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
        public static Aufgabe1Database GetDbInMemory()
        {
            SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            Aufgabe1Database db = new Aufgabe1Database(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        // TODO: Seed Database for Unit Tests
        public static void SeedDatabase(Aufgabe1Database db)
        {
            //List<Product> products = GetSeedingProducts();

            //db.Products.AddRange(products);

            //db.SaveChanges();
        }

        // TODO: Seeding Lists
        // You can finish and use this if you want,
        // or make your own seeding code
        //private static List<Product> GetSeedingProducts()
        //{
        //    return new List<Product>()
        //    {
        //        new Product(...),
        //        new Product(...),
        //        new Product(...)
        //        ...
        //    };
        //}
    }
}
