using Moq;
using ScsOnlineShop.Application.Infrastructure;
using ScsOnlineShop.Application.Model;
using ScsOnlineShop.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScsOnlineShop.Test
{
    [Collection("Sequential")]
    public class OrderServiceTests
    {
        [Fact]
        public void CheckoutSuccessTest()
        {
            using var db = TestHelpers.GetShopContext();
            var service = new OrderService(db);
            var customer = db.Customers.FirstOrDefault(c => c.Email == "muster@spengergasse.at");
            service.Checkout(customer.Id);
            Assert.True(db.Orders.Count() == 1);
            Assert.True(db.OrderItems.Count() == 1);
        }
    }
}
