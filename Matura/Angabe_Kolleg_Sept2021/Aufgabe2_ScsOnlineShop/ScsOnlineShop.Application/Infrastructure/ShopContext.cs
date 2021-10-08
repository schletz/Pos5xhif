using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Application.Infrastructure
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions opt) : base(opt) { }
    }
}
