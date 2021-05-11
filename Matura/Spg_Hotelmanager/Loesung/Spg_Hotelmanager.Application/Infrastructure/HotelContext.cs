//using Bogus;
//using Bogus.Extensions;
//using Bogus.Distributions.Gaussian;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spg_Hotelmanager.Application.Domain;

namespace Spg_Hotelmanager.Application.Infrastructure
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions options) : base(options) { }

        // Alles, was eine Tabelle werden soll, wird als DbSet definiert.
        // Der Propertyname (Customers, ...) bestimmt den Tabellennamen!
        public DbSet<Customer> Customers { get; set; }  // SELECT * FROM Persons WHERE Discriminator = 'Customer'
        public DbSet<Employee> Employees { get; set; }  // SELECT * FROM Persons WHERE Discriminator = 'Employee'
        public DbSet<Person> Persons { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        /// <summary>
        /// Abfrage eines Raumes auf Basis der Keycard Nummer. Rückgabetyp: Room
        /// </summary>
        // Mit C# 7:
        // public Room GetRoomByKeyCard(string keycardNumber) =>
        //    Rooms.FirstOrDefault(r => r.KeycardNumber == keycardNumber);/// 
        public Room GetRoomByKeyCard(string keycardNumber)
        {
            return Rooms.FirstOrDefault(r => r.KeycardNumber == keycardNumber);
        }

        /// <summary>
        /// Abfrage aller Räume einer Kategorie, die buchbar sind (Flag CanBeReserved).
        /// Rückgabetyp: IQueryable<Room>
        /// </summary>
        public IQueryable<Room> GetAvailableRooms(RoomCategory roomCategory)
        {
            // SELECT * FROM Rooms WHERE RoomCategory = 0 AND CanBeReserved = 1
            return Rooms.Where(r => r.RoomCategory == roomCategory && r.CanBeReserved);
        }

        /// <summary>
        /// Abfrage der Mitarbeiter, die vor einem übergebenen Datum in das Unternehmen
        /// eingetreten sind.Rückgabetyp: IQueryable<Employee>
        /// </summary>
        public IQueryable<Employee> GetEmployeesByEntryDate(DateTime entryDate)
        {
            return Employees.Where(e => e.EntryDate < entryDate);
        }

        /// <summary>
        /// Abfrage der Kunden, die keine Rechnungsadresse eingetragen haben (Die Felder
        /// Street, ZIP, City und CountryCode sind NULL). Rückgabetyp: IQueryable<Customer>
        /// </summary>
        public IQueryable<Customer> GetCustomersWithoutBillingAddress()
        {
            // Falsch:
            // return Customers.Where(c => c.BillingAddress == null);
            return Customers.Where(c =>
                c.BillingAddress.Street == null
                && c.BillingAddress.Zip == null
                && c.BillingAddress.City == null
                && c.BillingAddress.CountryCode == null);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Hotel.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguration der Value Objects in EF Core.
            modelBuilder.Entity<Person>().OwnsOne(p => p.Name);
            modelBuilder.Entity<Customer>().OwnsOne(c => c.HomeAddress);
            modelBuilder.Entity<Customer>().OwnsOne(c => c.BillingAddress);
        }

        // Method to fill the database with sample data. Can be ignored.
        //public void Seed()
        //{
        //    string[] countries = new string[] { "AT", "DE", "CZ", "SK", "HU", "SLO", "IT", "CH", "LUX" };

        //    Randomizer.Seed = new Random(900);
        //    int roomNr = 100;
        //    var rooms = new Faker<Room>("de").Rules((f, r) =>
        //    {
        //        r.RoomNumber = (roomNr++).ToString();
        //        r.KeycardNumber = f.Random.String2(8, "ABCDEF1234567890");
        //        r.RoomCategory = f.Random.Enum<RoomCategory>();
        //        r.PricePerNight = 300 + 200 * (int)r.RoomCategory;
        //        r.CanBeReserved = f.Random.Bool(0.8f);
        //    })
        //    .Generate(20)
        //    .ToList();

        //    Rooms.AddRange(rooms);
        //    SaveChanges();

        //    var employees = new Faker<Employee>("de").Rules((f, e) =>
        //    {
        //        e.Name = new Name { Firstname = f.Name.FirstName(), Lastname = f.Name.LastName() };
        //        e.EntryDate = new DateTime(1990, 1, 1).AddDays(f.Random.Int(0, 20 * 365));
        //    })
        //    .Generate(10)
        //    .ToList();
        //    Employees.AddRange(employees);
        //    SaveChanges();

        //    var customers = new Faker<Customer>("de").Rules((f, c) =>
        //    {
        //        var country = f.Random.ListItem(countries);
        //        c.Name = new Name { Firstname = f.Name.FirstName(), Lastname = f.Name.LastName() };
        //        c.HomeAddress = new Address { Street = f.Address.StreetAddress(), City = f.Address.City(), Zip = f.Address.ZipCode(), CountryCode = country };
        //        if (f.Random.Bool(0.2f))
        //        {
        //            c.BillingAddress = new Address { Street = f.Address.StreetAddress(), City = f.Address.City(), Zip = f.Address.ZipCode(), CountryCode = country };
        //        }
        //        c.PassportNumber = country + f.Random.String2(8, "ABCDEF1234567890");
        //        c.Reservations = new Faker<Reservation>("de").Rules((f2, r) =>
        //        {
        //            r.Employee = f2.Random.ListItem(employees);
        //            r.ReservationFrom = f.Date.Between(new DateTime(2019, 1, 1), new DateTime(2021, 8, 1)).Date;
        //            r.ReservationTo = r.ReservationFrom.AddDays(f2.Random.Int(3, 12));
        //            if (r.ReservationFrom < new DateTime(2021, 4, 1))
        //            {
        //                r.Room = f2.Random.ListItem(rooms);
        //                r.InvoiceAmount = r.Room.PricePerNight * (int)(r.ReservationTo - r.ReservationFrom).TotalDays + Math.Max(0, Math.Round(f2.Random.GaussianDecimal(300, 100), 2));
        //            }
        //        })
        //        .Generate(f.Random.Int(1, 6))
        //        .ToList();
        //    })
        //    .Generate(25)
        //    .ToList();
        //    Customers.AddRange(customers);
        //    SaveChanges();
        //}
    }
}
