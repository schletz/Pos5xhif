using Microsoft.EntityFrameworkCore;
using Spg_Hotelmanager.Application.Infrastructure;
using System;
using Xunit;
using System.Linq;

namespace Spg_Hotelmanager.Test
{
    public class DbContextTests
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
        public void DbCreationTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Hotel.db")
                .Options;

            using (var db = new HotelContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Import("data.sql");
                //db.Seed();
                Assert.True(true);
            }
        }

        [Fact]
        public void GetRoomByKeyCardSuccessTest()
        {
            using (var db = GetContext())
            {
                var room = db.GetRoomByKeyCard("323AF540");
                Assert.True(room.KeycardNumber == "323AF540");
            }
        }

        [Fact]
        public void GetRoomsByCategorySuccessTest()
        {
            using (var db = GetContext())
            {
                var rooms = db.GetRoomsByCategory(Application.Domain.RoomCategory.Basic);
                Assert.True(rooms.Count() == 7);
            }
        }

        [Fact]
        public void GetEmployeesEntersBeforeSuccessTest()
        {
            using (var db = GetContext())
            {
                var rooms = db.GetEmployeesEntersBefore(new DateTime(2000,1,1));
                Assert.True(rooms.Count() == 4);
            }
        }

        [Fact]
        public void GetCustomersWithoutBillingAddressSuccessTest()
        {
            using (var db = GetContext())
            {
                var rooms = db.GetCustomersWithoutBillingAddress();
                Assert.True(rooms.Count() == 21);
            }
        }
    }
}
