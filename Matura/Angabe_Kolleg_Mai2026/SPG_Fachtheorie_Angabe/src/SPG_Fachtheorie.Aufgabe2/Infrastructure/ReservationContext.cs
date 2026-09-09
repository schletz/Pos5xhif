using Bogus;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SPG_Fachtheorie.Aufgabe2.Infrastructure
{
    public class ReservationContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Status> States => Set<Status>();
        public DbSet<Model.Person> Persons => Set<Model.Person>();
        public DbSet<Staff> Staffs => Set<Staff>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        public ReservationContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Tabellennamen basieren auf Entity-Namen (nicht DbSet) und starten klein
                var clrName = entityType.ClrType.Name;
                var tableName = char.ToLowerInvariant(clrName[0]) + clrName[1..];
                entityType.SetTableName(tableName);

                foreach (var property in entityType.GetProperties())
                {
                    var propName = property.Name;
                    // Spaltenname: Nur erster Buchstabe klein
                    property.SetColumnName(char.ToLowerInvariant(propName[0]) + propName[1..]);

                    // Standardkonfigurationen
                    if (property.ClrType == typeof(string) && property.GetMaxLength() is null)
                        property.SetMaxLength(255);

                    if (property.ClrType == typeof(DateTime)) property.SetPrecision(3);
                    if (property.ClrType == typeof(DateTime?)) property.SetPrecision(3);
                }

                // FK-Verhalten
                foreach (var fk in entityType.GetForeignKeys())
                    fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1938);
            var faker = new Faker("de");

            var states = new Status[]
            {
                new Status("Pending"),
                new Status("Issued"),
                new Status("Damaged return")
            };
            States.AddRange(states);
            SaveChanges();

            var categories = new Category[]
            {
                new Category("Notebooks"),
                new Category("Adapters"),
                new Category("Cables"),
            };
            Categories.AddRange(categories);
            SaveChanges();

            var schoolClasses = new string[] { "5AAIF", "5BAIF", "5CAIF", "8ABIF", "8ACIF", "5AKIF", "5BKIF" };
            var staffs = Generate(f => new Staff(f.Person.FirstName, f.Person.LastName), 3);
            var persons = Generate(f => new Model.Person(
                f.Person.FirstName, f.Person.LastName, f.Random.ListItem(schoolClasses),
                f.Internet.Email(), f.Phone.PhoneNumber()), 20);
            var inventoryItems = Generate(f => new InventoryItem(
                f.Random.String2(10, "0123456789ABCDEF"), f.Commerce.ProductName(), f.Random.ListItem(categories), f.Random.Bool(0.8f)), 10);
            var reservations = Generate(f =>
            {
                var loanDate = f.Date.BetweenDateOnly(new DateOnly(2025, 9, 1), new DateOnly(2026, 2, 1));
                var returnDate = loanDate.AddDays(f.Random.Int(1, 7)).OrNull(f, 0.1f);
                var acceptedBy = returnDate.HasValue ? f.Random.ListItem(staffs) : null;
                var status = returnDate.HasValue ? f.Random.ListItem(states.Skip(1).ToList()) : states[0];
                return new Reservation(
                    f.Random.ListItem(persons), f.Random.ListItem(inventoryItems),
                    loanDate, status, returnDate, acceptedBy);
            }, 20);
        }
        private List<T> Generate<T>(Func<Faker, T> generator, int count) where T : class
        {
            var data = new Faker<T>("de")
                .CustomInstantiator(generator)
                .Generate(count)
                .ToList();
            var set = Set<T>();
            set.AddRange(data);
            SaveChanges();
            return data;
        }

        private List<T> GenerateDistinct<T, Tkey>(Func<Faker, T> generator, int count, params Func<T, Tkey>[] distinctBys) where T : class
        {
            var dataEnumerable = new Faker<T>("de")
                .CustomInstantiator(generator)
                .GenerateForever();
            foreach (var distinctBy in distinctBys)
                dataEnumerable = dataEnumerable.DistinctBy(distinctBy);
            var data = dataEnumerable.Take(count).ToList();
            var set = Set<T>();
            set.AddRange(data);
            SaveChanges();
            return data;
        }

        public void WriteToTsv(string path)
        {
            var dump = Database.GenerateCreateScript();
            File.WriteAllText(
                Path.Combine(path, "dump.sql"),
                dump,
                new UTF8Encoding(false, false));
            WriteEntitiesToTsv(nameof(Category), path);
            WriteEntitiesToTsv(nameof(Status), path);
            WriteEntitiesToTsv(nameof(Aufgabe2.Model.Person), path);
            WriteEntitiesToTsv(nameof(Staff), path);
            WriteEntitiesToTsv(nameof(InventoryItem), path);
            WriteEntitiesToTsv(nameof(Reservation), path);
        }

        private void WriteEntitiesToTsv(string tableName, string path)
        {
            var encoder = new UTF8Encoding(false, false);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
                NewLine = "\r\n",
            };

            using var command = Database.GetDbConnection().CreateCommand();
            command.CommandText = $"SELECT * FROM {tableName}";

            using var reader = command.ExecuteReader();
            using var writer = new StreamWriter(Path.Combine(path, $"{tableName.ToLower()}.tsv"), false, encoder);
            using var csv = new CsvWriter(writer, config);

            for (int i = 0; i < reader.FieldCount; i++)
                csv.WriteField(reader.GetName(i));

            csv.NextRecord();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var value = reader.GetValue(i);
                    if (value == DBNull.Value)
                        csv.WriteField(string.Empty);
                    else if (value is DateTime dt)
                        csv.WriteField(dt.ToString("s")); // ISO 8601 Format (YYYY-MM-DDTHH:MM:SS)
                    else if (value is DateOnly d)
                        csv.WriteField(d.ToString("yyyy-MM-dd"));
                    else
                        csv.WriteField(value);
                }
                csv.NextRecord();
            }
        }
    }
}
