using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ScsOnlineShop.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ScsOnlineShop.Test
{
    public class ShopContextTests
    {
        [Fact]
        public void CreateDatabaseSuccessTest()
        {
            var opt = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Shop.db")
                .Options;

            using var db = new ShopContext(opt);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
