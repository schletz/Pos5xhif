using Microsoft.EntityFrameworkCore;
using Spg_Fahrzeugvermietung.Application.Infrastructure;
using System;
using System.IO;
using Xunit;

namespace Spg_Fahrzeugvermietung.Test
{
    public class DbContextTests
    {
        [Fact]
        public void DbCreationTest()
        {
            var assembly = typeof(DbContextTests).Assembly.GetName().Name;
            var currentPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf(assembly));
            using (var db = new FahrzeugContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Import("data.sql");
                //db.Seed();
                Assert.True(true);
            }
        }
    }
}
