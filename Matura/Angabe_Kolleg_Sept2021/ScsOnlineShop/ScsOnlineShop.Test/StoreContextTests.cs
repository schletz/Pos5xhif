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
    [Collection("Sequential")]
    public class StoreContextTests
    {
        private StoreContext GetStoreContext()
        {
            var opt = new DbContextOptionsBuilder()
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=Store.db")
                .Options;
            var db = new StoreContext(opt);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
        [Fact]
        public void EnsureCreatedSuccessTest()
        {
            using var db = GetStoreContext();
        }

        [Fact]
        public void IsInBusinessTimeSuccessTest()
        {
            using var db = GetStoreContext();
            // Arrange
            var store = new ActiveStore(new Store(
                location: "Top 10",
                floor: "1 OG",
                companyName: "Zara",
                state: StoreState.Active,
                deliveryOption: DeliveryOption.Postal));
            var businessHour = new BusinessHour(
                new OpeningHour(
                    dayOfWeek: DayOfWeek.Monday, hourFrom: 8, hourTo: 16, store: store));
            db.BusinessHours.Add(businessHour);
            db.SaveChanges();
            // Act & Assert
            Assert.True(store.IsInBusinessTime(new DateTime(2021, 10, 4, 10, 0, 0)));
        }
        [Fact]
        public void IsInBusinessTimeReturnsFalseTest()
        {
            using var db = GetStoreContext();
            // Arrange
            var store = new ActiveStore(new Store(
                location: "Top 10",
                floor: "1 OG",
                companyName: "Zara",
                state: StoreState.Active,
                deliveryOption: DeliveryOption.Postal));
            var businessHour = new BusinessHour(
                new OpeningHour(
                    dayOfWeek: DayOfWeek.Monday, hourFrom: 8, hourTo: 16, store: store));
            db.BusinessHours.Add(businessHour);
            db.SaveChanges();
            // Act & Assert
            Assert.False(store.IsInBusinessTime(new DateTime(2021, 10, 4, 16, 10, 0)));
        }
    }
}
