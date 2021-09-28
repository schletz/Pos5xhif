using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Domain;
using System;
using System.Collections.Generic;
using Bogus;
using Bogus.Extensions;
using Bogus.Distributions.Gaussian;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    /// <summary>
    /// Datenbankkontext für die Aufgabe 1.
    /// Hinweise:
    ///     Registrieren Sie die verwendeten Tabellen und führen Sie nötige Konfigurationen durch.
    ///     Der Test GenerateDbFromSqlFileTest im Testprojekt muss erfolgreich durchlaufen.
    ///     Schreiben Sie die geforderten Abfragen direkt in den DbContext.
    ///     Verwenden Sie die bereitgestellte Klasse DbContextTests im Testprojekt, um die Implementierung zu testen.
    /// </summary>
    public class CovidTestContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<TestStation> TestStations { get; set; }
        public DbSet<Domain.Person> Persons { get; set; }
        public DbSet<Test> Tests { get; set; }
        public CovidTestContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Person>().OwnsOne(p => p.PhoneNumber);
        }

        /// <summary>
        /// Aufgabe 1c - Abfrage einer Person auf Basis seiner Telefonnummer.
        /// Hinweis:
        ///     Geben Sie den ersten gefundenen Datensatz zurück, da theoretisch auch 2 Personen die gleiche Nummer haben können.
        ///     Eine Telefonnummer ist gleich, wenn alle Nummernteile gleich sind.
        ///     Wird keine Person gefunden, ist null zu liefern.
        /// </summary>
        public Domain.Person GetPersonByPhoneNumber(PhoneNumber phoneNumber)
        {
            return Persons.FirstOrDefault(p =>
                p.PhoneNumber.CountryCode == phoneNumber.CountryCode
                && p.PhoneNumber.AreaCode == phoneNumber.AreaCode
                && p.PhoneNumber.SerialNumber == phoneNumber.SerialNumber);
        }

        /// <summary>
        /// Aufgabe 1c - Abfrage von Personen auf Basis der Postleitzahl.
        /// </summary>
        public IQueryable<Domain.Person> GetPersonByZipCode(string zipCode)
        {
            return Persons.Where(p => p.Address.ZipCode == zipCode);
        }

        /// <summary>
        /// Abfrage von Personen auf Basis eines Ortsnamensteils.
        /// </summary>
        public IQueryable<Domain.Person> GetPersonCityContains(string city)
        {
            return Persons.Where(p => p.Address.City.Contains(city));
        }

        /// <summary>
        /// Eine Statistikabfrage, die die Anzahl pro TestKitType und pro TestResult für einen Zeitraum zurückgibt.
        /// Hinweis: Die übergebenen Zeitwerte sind inklusive zu vergleichen.
        /// </summary>
        public int GetTestCount(TestKitType testKitType, TestResult testResult, DateTime timestampFrom, DateTime timestampTo)
        {
            return Tests.Count(t =>
                t.TestKitType == testKitType
                && t.Result == testResult
                && t.TestTimeStamp >= timestampFrom
                && t.TestTimeStamp <= timestampTo);
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(819);
            var testStationNames = new string[] { "Wien Austria Center", "Wien Stadthalle", "Wien Messehalle" };
            var countryCodes = new int[] { 43, 49, 30 };
            var countries = new string[] { "AT", "DE", "H" };
            var countriesWeights = new float[] { 0.8f, 0.1f, 0.1f };

            var testStations = testStationNames.Select(t => new TestStation { StationName = t }).ToList();
            TestStations.AddRange(testStations);
            SaveChanges();

            var addresses = new Faker<Address>("de").Rules((f, a) =>
            {
                a.StreetNumber = f.Address.StreetAddress();
                a.ZipCode = f.Random.Int(1000, 9999).ToString();
                a.City = f.Address.City();
                a.Country = f.Random.WeightedRandom(countries, countriesWeights);
                a.Persons = new Faker<Domain.Person>("de").Rules((f2, p) =>
                {
                    p.FirstName = f2.Name.FirstName();
                    p.LastName = f2.Name.LastName();
                    var genderWeight = f2.Random.Int(0, 100);
                    p.Gender = genderWeight < 45 ? Gender.MALE : genderWeight < 90 ? Gender.FEMALE : Gender.DIVERS;
                    var birthDate = DateTime.MaxValue;
                    while (birthDate > new DateTime(2015, 1, 1))
                    {
                        birthDate = new DateTime(1976, 1, 1).AddDays(f2.Random.GaussianInt(0, 8218));
                    }
                    p.BirthDate = birthDate;
                    p.SocialSecurityNummer = f2.Random.Int(100000000, 999999999);
                    p.Email = p.LastName.ToLower() + "@" + f2.Internet.DomainName();
                    p.PhoneNumber = new PhoneNumber
                    {
                        CountryCode = f2.Random.ListItem(countryCodes),
                        AreaCode = f2.Random.Int(1000, 9999),
                        SerialNumber = f2.Random.Int(100000, 999999)
                    };
                    p.Tests = new Faker<Test>("de").Rules((f3, t) =>
                    {
                        t.TestTimeStamp = new DateTime(2021, 1, 1).AddSeconds(f3.Random.Int(0, 183 * 86400));
                        t.Station = f3.Random.ListItem(testStations);
                        t.TestBay = t.TestTimeStamp < new DateTime(2021, 5, 27) ? f3.Random.Int(1, 5) : null as int?;
                        t.TestKitType = t.TestTimeStamp < new DateTime(2021, 5, 27) ? f3.Random.Enum<TestKitType>() : null as TestKitType?;
                        t.Result = t.TestTimeStamp < new DateTime(2021, 5, 27) ? f3.Random.Enum<TestResult>() : null as TestResult?;
                    })
                    .Generate(p.Gender == Gender.FEMALE ? f2.Random.Int(1, 4) : f2.Random.Int(1, 2))
                    .ToList();
                })
                .Generate(f.Random.Int(100, 220) / 100)
                .ToList();
            })
            .Generate(100);
            Addresses.AddRange(addresses);
            SaveChanges();
        }
    }

}
