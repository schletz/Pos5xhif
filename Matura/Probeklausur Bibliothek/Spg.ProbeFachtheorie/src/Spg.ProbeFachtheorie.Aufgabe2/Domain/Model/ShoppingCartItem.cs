using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Model
{
    public enum ShoppingCartItemStates { Avaiable = 1, Reserved = 2, Taken = 3 }
    public class ShoppingCartItem : EntityBase
    {
        public int ShoppingCartItemStateId { get; set; }
        public ShoppingCartItemStates ShoppingCartItemState { get; set; } = default!;
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = default!;
        public int BookId { get; set; }
        public Book Book { get; set; } = default!;
    }
}
