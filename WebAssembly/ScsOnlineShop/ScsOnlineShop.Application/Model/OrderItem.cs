namespace ScsOnlineShop.Application.Model
{
    public class OrderItem
    {
        public OrderItem(int quantity, Product product, decimal price, Order order)
        {
            Quantity = quantity;
            ProductEan = product.Ean;
            Product = product;
            Price = price;
            OrderId = order.Id;
            Order = order;
        }
        protected OrderItem() { }
        public int Id { get; private set; }
        public int Quantity { get; set; }
        public int ProductEan { get; set; }
        public virtual Product Product { get; set; } = default!;
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = default!;
    }
}
