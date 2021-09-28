using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using System;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    [Collection("Sequential")]
    public class DbContextTests
    {
        [Fact]
        public void GenerateDbFromContextTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Store.db")
                .Options;

            using (var db = new StoreContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Assert.True(true);
            }
        }
    }
}
