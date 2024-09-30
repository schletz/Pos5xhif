using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.Fachtheorie.Aufgabe2.Services;

namespace Spg.Fachtheorie.Aufgabe2.Test.Helpers
{
    public class DatabaseUtilities
    {
        public static Aufgabe2Database GetDbFile()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source = C:\\Scratch\\Aufgabe2_Tests.db")
                .Options;

            Aufgabe2Database db = new Aufgabe2Database(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
        public static Aufgabe2Database GetDbInMemory()
        {
            SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            Aufgabe2Database db = new Aufgabe2Database(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        // TODO: Seed Database for Unit Tests
        public static void SeedDatabase(Aufgabe2Database db)
        {
            //List<Product> products = GetSeedingProducts();

            //db.Products.AddRange(products);

            //db.SaveChanges();
        }
    }
}
