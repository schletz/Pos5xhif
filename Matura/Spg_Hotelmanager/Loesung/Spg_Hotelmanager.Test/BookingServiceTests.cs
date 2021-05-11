using Microsoft.EntityFrameworkCore;
using Spg_Hotelmanager.Application.Infrastructure;
using Spg_Hotelmanager.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Spg_Hotelmanager.Test
{
    public class BookingServiceTests
    {
        private HotelContext GetContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Hotel.db")
                .Options;
            var db = new HotelContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Import("data.sql");
            return db;
        }

        [Fact]
        public void GetPreferredRoomSuccessTest()
        {
            using(var db = GetContext())
            {
                var serice = new BookingService(db);
                var roomId = serice.GetPreferredRoom(11, Application.Domain.RoomCategory.Basic);
                Assert.True(roomId == 18);
            }
        }

        [Fact]
        public void GetCustomerRevenueCategoriesSuccessTest()
        {
            using (var db = GetContext())
            {
                var serice = new BookingService(db);
                var result = serice.GetCustomerRevenueCategories();
                Assert.True(result.Count == 3);
            }
        }
    }
}
