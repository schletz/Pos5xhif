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

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        /// <summary>
        /// Abfrage eines Raumes auf Basis der Keycard Nummer. Rückgabetyp: Room
        /// </summary>
        /// 
        //public Room GetRoomByKeyCard(string keyCardNumber) =>
        //    Rooms.FirstOrDefault(r => r.KeycardNumber == keyCardNumber);
        public Room GetRoomByKeyCard(string keyCardNumber)
        {
            return Rooms.FirstOrDefault(r => r.KeycardNumber == keyCardNumber);
        }

        /// <summary>
        /// Abfrage aller Räume einer Kategorie, die buchbar sind (Flag CanBeReserved).
        /// Rückgabetyp: IQueryable<Room>
        /// </summary>
        public IQueryable<Room> GetRoomsByCategory(RoomCategory roomCategory)
        {
            return Rooms.Where(r => r.CanBeReserved && r.RoomCategory == roomCategory);
        }

        /// <summary>
        /// Abfrage der Mitarbeiter, die vor einem übergebenen Datum in das Unternehmen
        /// eingetreten sind.Rückgabetyp: IQueryable<Employee>
        /// </summary>

        public IQueryable<Employee> GetEmployeesEntersBefore(DateTime entryDate)
        {
            return Employees.Where(e => e.EntryDate < entryDate);
        }


        /// <summary>
        /// Abfrage der Kunden, die keine Rechnungsadresse eingetragen haben (Die Felder
        /// Street, ZIP, City und CountryCode sind NULL). Rückgabetyp: IQueryable<Customer>
        /// </summary>
        /// 
        public IQueryable<Customer> GetCustomersWithoutBillingAddress()
        {
            // Falsch: return Customers.Where(c => c.BillingAddress == null);
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
            modelBuilder.Entity<Person>().OwnsOne(p => p.Name);
            modelBuilder.Entity<Customer>().OwnsOne(p => p.HomeAddress);
            modelBuilder.Entity<Customer>().OwnsOne(p => p.BillingAddress);
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
