using Bogus;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Infrastructure
{
    public class ScooterContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Scooter> Scooters => Set<Scooter>();
        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<TripLog> TripLogs => Set<TripLog>();

        public ScooterContext()
        { }

        public ScooterContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=scooter.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Generic config for all entities
                // ON DELETE RESTRICT instead of ON DELETE CASCADE
                foreach (var key in entityType.GetForeignKeys())
                    key.DeleteBehavior = DeleteBehavior.Restrict;

                foreach (var prop in entityType.GetDeclaredProperties())
                {
                    // Define Guid as alternate key. The database can create a guid fou you.
                    if (prop.Name == "Guid")
                    {
                        modelBuilder.Entity(entityType.ClrType).HasAlternateKey("Guid");
                        prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                    }
                    // Default MaxLength of string Properties is 255.
                    if (prop.ClrType == typeof(string) && prop.GetMaxLength() is null) prop.SetMaxLength(255);
                    // Seconds with 3 fractional digits.
                    if (prop.ClrType == typeof(DateTime)) prop.SetPrecision(3);
                    if (prop.ClrType == typeof(DateTime?)) prop.SetPrecision(3);
                }
            }
            modelBuilder.Entity<Trip>().OwnsOne(t => t.ParkingLocation);
            modelBuilder.Entity<TripLog>().OwnsOne(t => t.Location);
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(2015);
            var faker = new Faker("de");

            var scooters = new Scooter[]
            {
                new Scooter("Xiaomi", "Mi Electric Scooter Pro 2", 0.15m),
                new Scooter("Segway", "Ninebot Max G30", 0.20m),
                new Scooter("Bird", "Bird One", 0.18m),
            };
            Scooters.AddRange(scooters);
            SaveChanges();

            var users = new User[]
            {
                new User("alice@example.com", 0),
                new User("bob@example.com", 7),
                new User("charlie@example.com", 0)
            };
            Users.AddRange(users);
            SaveChanges();

            foreach (var scooter in scooters)
            {
                int mileage = faker.Random.Int(0, 30000);
                var trips = new Faker<Trip>("de")
                    .CustomInstantiator(f =>
                    {
                        var user = f.Random.ListItem(users);
                        var begin = f.Date.Between(new DateTime(2025, 1, 1), new DateTime(2025, 2, 28));
                        begin = new DateTime(begin.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond);
                        var end = begin.AddSeconds(f.Random.Int(3 * 60, 3 * 3600)).OrNull(f, scooter.Id == 3 ? 0.0f : 0.5f);
                        var parkingLocation = end.HasValue ? new Location(
                            f.Random.Int(1500000, 1700000) / 100000M,
                            f.Random.Int(4800000, 4850000) / 100000M) : null;
                        var trip = new Trip(user, scooter, begin, end, parkingLocation);
                        var timestamp = begin;
                        while (timestamp < (end ?? begin.AddMinutes(10)))
                        {
                            var location = new Location(
                                f.Random.Int(1500000, 1700000) / 100000M,
                                f.Random.Int(4800000, 4850000) / 100000M);
                            trip.TripLogs.Add(new TripLog(trip, timestamp, location, mileage));
                            mileage += f.Random.Int(0, 1000);
                            timestamp = timestamp.AddMinutes(f.Random.Int(1, 5));
                        }
                        return trip;
                    })
                    .Generate(faker.Random.Int(4, 10))
                    .ToList();
                Trips.AddRange(trips);
                SaveChanges();
            }
        }
    }
}