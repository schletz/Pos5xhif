using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
