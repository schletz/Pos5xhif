using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Domain;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    /// <summary>
    /// Tests für die Klasse CovidTestContext. Verwenden Sie die
    /// vordefinierten Testmethoden. Sie können die Methode GetContext()
    /// nutzen, um eine befüllte Datenbank zu bekommen.
    /// Die Datenbank liegt ebenfalls in der Datei CovidTest.db im
    /// Ordner der sln Datei.
    /// </summary>
    [Collection("Sequential")]
    public class DbContextTests
    {
        private CovidTestContext GetContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=CovidTest.db")
                .Options;
            var db = new CovidTestContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Import("data.sql");
            return db;
        }

        [Fact]
        public void GenerateDbFromContextTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=CovidTest.db")
                .Options;

            using (var db = new CovidTestContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Seed();
                Assert.True(true);
            }
        }

        [Fact]
        public void GenerateDbFromSqlFileTest()
        {
            using (var db = GetContext())
            {
                Assert.True(db.Tests.Count() > 0);
            }
        }

        [Fact]
        public void GetPersonByPhoneNumberTest()
        {
            using (var db = GetContext())
            {
                var person = db.Persons.First();
                Assert.True(person.Id == db.GetPersonByPhoneNumber(person.PhoneNumber)?.Id);
            }
        }
        [Fact]
        public void GetPersonByZipCodeTest()
        {
            using (var db = GetContext())
            {
                Assert.True(db.GetPersonByZipCode("7273").Count() == 2);
            }
        }

        /// <summary>
        /// Hinweis: Verwenden Sie ein kleines SQL Statement, um die korrekte Anzahl mit
        /// DBeaver herauszufinden:
        /// SELECT COUNT(*) FROM Persons p INNER JOIN Addresses a ON(p.AddressId == a.Id) WHERE a.City  LIKE '%ad%';
        /// </summary>
        [Fact]
        public void GetPersonCityContains()
        {
            using (var db = GetContext())
            {
                var count = db.GetPersonCityContains("ad").Count();
                Assert.True(count == 31);
            }
        }

        [Fact]
        public void GetTestCountTest()
        {
            using (var db = GetContext())
            {
                var count = db.GetTestCount(TestKitType.PCR, TestResult.POSITIVE, new DateTime(2021, 2, 1), new DateTime(2021, 3, 1));
                Assert.True(count == 2);
            }
        }

    }
}
