using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Model
{
    public class PickupTime : EntityBase
    {
        public DateTime Date { get; set; }
        public Guid Guid { get; set; }


        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = default!;


        public int PicupUserId { get; set; }
        public User PicupUser { get; set; } = default!;
    }
}
