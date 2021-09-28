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
            using (var db = new FahrzeugContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Import("data.sql");
                Assert.True(true);
            }
        }
    }
}
