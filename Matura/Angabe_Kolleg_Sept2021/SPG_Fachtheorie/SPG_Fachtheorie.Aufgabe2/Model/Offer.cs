using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    [Table("Offer")]
    public class Offer
    {
        public int Id { get; set; }
        public int ProductEan { get; set; }
        public Product Product { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
