using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScsOnlineShop.Test
{
    // Die Filedatenbank kann nicht parallel gelöscht werden.
    [Collection("Sequential")]
    public class CustomerTests
    {
        [Fact]
        public void AddToShoppingCartReturnsTrueTest()
        {
            using var db = TestHelpers.GetShopContext();
            var customer = db.Customers.FirstOrDefault(c => c.Email == "muster@spengergasse.at");
            var offer = db.Offers.FirstOrDefault();
            customer!.AddToShoppingCart(offer!, 1);
            db.SaveChanges();
            Assert.True(db.ShoppingCarts.Any(s => 
                s.CustomerId == customer.Id 
                && s.OfferId == offer!.Id 
                && s.Quantity == 1));
        }
    }
}
