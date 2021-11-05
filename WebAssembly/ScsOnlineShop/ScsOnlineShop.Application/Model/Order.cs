using System;
using System.Collections.Generic;

namespace ScsOnlineShop.Application.Model
{
    public class Order
    {
        public Order(DateTime orderDate, Store store, Customer customer)
        {
            OrderDate = orderDate;
            StoreId = store.Id;
            Store = store;
            CustomerId = customer.Id;
            Customer = customer;
        }
        protected Order() { }
        public int Id { get; private set; }
        public DateTime OrderDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = default!;
        public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>(0);
    }
}
