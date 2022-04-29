using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Model
{
    public class ShoppingCart : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public Guid Guid { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;
    }
}
