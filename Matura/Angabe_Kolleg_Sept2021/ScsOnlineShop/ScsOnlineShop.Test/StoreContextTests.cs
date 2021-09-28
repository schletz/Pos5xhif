using ScsOnlineShop.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ScsOnlineShop.Test
{
    public class StoreContextTests
    {
        [Fact]
        public void EnsureCreatedSuccessTest()
        {
            var opt = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Store.db")
                .Options;
            using var db = new StoreContext(opt);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
