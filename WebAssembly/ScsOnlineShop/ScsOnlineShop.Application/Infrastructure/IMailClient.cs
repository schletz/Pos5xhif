using ScsOnlineShop.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Application.Infrastructure
{
    public interface IMailClient
    {
        Task<(bool success, string? message)> SendCustomerMail(Customer c, string mailtext);
    }
}
