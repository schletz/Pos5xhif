using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    [Table("OrderItem")]

    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductEan { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
