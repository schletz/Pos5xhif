using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    [Collection("Sequential")]
    public class DbContextTests
    {
        private StoreContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Store.db")
                .Options;

            var db = new StoreContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();
            return db;
        }

        [Fact]
        public void GenerateDbFromContextTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite($"Data Source=Store.db")
                .Options;

            using (var db = GetDbContext())
            {
                Assert.True(true);
            }
        }
    }
}
