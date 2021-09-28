using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    /// <summary>
    /// Fügen Sie hier Ihre Unittests in die entsprechenden Methoden ein.
    /// </summary>
    [Collection("Sequential")]
    public class OrderServiceTests
    {
        /// <summary>
        /// Liefert eine Instanz des DB Contextes, welche zum Testen der Services benötigt wird.
        /// </summary>
        /// <returns></returns>
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
        public void AddToShoppingCartNewOfferSuccessTest()
        {

        }
        [Fact]
        public void AddToShoppingCartExistingOfferSuccessTest()
        {

        }
        [Fact]
        public void AddToShoppingCartInvalidCustomerTest()
        {

        }
        [Fact]
        public void AddToShoppingCartInvalidOfferTest()
        {

        }
        [Fact]
        public void RemoveFromShoppingCartSuccessTest()
        {

        }
        [Fact]
        public void CheckoutSuccessTest()
        {

        }
        [Fact]
        public void CheckoutInvalidCustomerTest()
        {

        }
        [Fact]
        public void CheckoutEmptyShoppingCartTest()
        {

        }
    }
}
