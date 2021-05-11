using Microsoft.EntityFrameworkCore;
using Spg_Schoolrating.Application.Infrastructure;
using System;
using Xunit;

namespace Spg_Schoolrating.Test
{
    public class DbContextTests
    {
        [Fact]
        public void DbCreationTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Rating.db")
                .Options;

            using (var db = new RatingContext(options))
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
