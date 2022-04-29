using Microsoft.EntityFrameworkCore;
using Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;
using Spg.ProbeFachtheorie.Aufgabe2.Services;
using System;
using System.Linq;
using Xunit;

namespace Spg.ProbeFachtheorie.Aufgabe2.Test
{
    [Collection("Sequential")]
    public class PickupTimeServiceTests
    {
        private LibraryContext GetContext()
        {
            var opt = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Library.db")
                .Options;
            var db = new LibraryContext(opt);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();
            return db;
        }

        /// <summary>
        /// Demotest zum Testen einer Servicemethode.
        /// </summary>
        [Fact]
        public void GetBooksSuccessTest()
        {
            using var db = GetContext();
            var service = new PickupTimeService(db);
            var count = service.GetAllBooks().Count();
            Assert.True(200);
        }
    }
}
