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
        public async Task CheckoutSuccessTest()
        {
            // NuGet: Paket Moq installieren
            var mock = new Mock<IMailClient>();
            mock.Setup(client => client
                .SendCustomerMail(It.IsAny<Customer>(), It.IsAny<string>()).Result)
                .Returns((true, null));
            using var db = TestHelpers.GetShopContext();
            var service = new OrderService(db, mock.Object);
            var customer = db.Customers.FirstOrDefault(c => c.Email == "muster@spengergasse.at");
            await service.Checkout(customer.Id);
            Assert.True(db.Orders.Count() == 1);
            Assert.True(db.OrderItems.Count() == 1);
        }
    }
}
