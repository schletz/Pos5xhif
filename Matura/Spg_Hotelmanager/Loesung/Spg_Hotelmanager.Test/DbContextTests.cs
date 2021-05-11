using Microsoft.EntityFrameworkCore;
using Spg_Hotelmanager.Application.Infrastructure;
using System;
using Xunit;
using System.Linq;

namespace Spg_Hotelmanager.Test
{
    public class DbContextTests
    {
        /// <summary>
        /// "Mockup" der Datenbank
        /// </summary>
        /// <returns></returns>
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
            // Arrange: Erzeugen der Musterdatenbank
            using (var db = GetContext())
            {
                // Act
                var room = db.GetRoomByKeyCard("8CE09B8F");
                // Assert
                Assert.True(room.KeycardNumber == "8CE09B8F");
            }
        }

        [Fact]
        public void GetAvailableRoomsSuccessTest()
        {
            using (var db = GetContext())
            {
                // Act
                var room = db.GetAvailableRooms(Application.Domain.RoomCategory.Superior);
                // Assert
                Assert.True(room.Count() == 7);
            }
        }

        [Fact]
        public void GetEmployeesByEntryDateSuccessTest()
        {
            using (var db = GetContext())
            {
                // Act
                var room = db.GetEmployeesByEntryDate(new DateTime(2000,1,1));
                // Assert
                Assert.True(room.Count() == 4);
            }
        }

        [Fact]
        public void GetCustomersWithoutBillingAddressSuccessTest()
        {
            using (var db = GetContext())
            {
                // Act
                var room = db.GetCustomersWithoutBillingAddress();
                // Assert
                Assert.True(room.Count() == 21);
            }
        }
    }
}
