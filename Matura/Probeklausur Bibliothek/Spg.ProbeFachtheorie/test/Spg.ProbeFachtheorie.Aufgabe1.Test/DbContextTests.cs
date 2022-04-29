using Microsoft.EntityFrameworkCore;
using Spg.ProbeFachtheorie.Aufgabe1.Infrastructure;
using System;
using Xunit;

namespace Spg.ProbeFachtheorie.Aufgabe1.Test
{
    [Collection("Sequential")]
    public class DbContextTests
    {
        [Fact]
        public void GenerateDbFromContextTest()
        {
            var opt = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Library.db")
                .Options;
            using var db = new LibraryContext(opt);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            Assert.True(true);
        }
    }
}
