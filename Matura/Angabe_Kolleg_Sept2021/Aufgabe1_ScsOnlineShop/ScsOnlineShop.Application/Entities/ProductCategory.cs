namespace ScsOnlineShop.Application.Entities
{

    public class ProductCategory
    {
        private ProductCategory() { }
        public ProductCategory(string name, bool canBeDelivered, ActiveStore store)
        {
            Name = name;
            CanBeDelivered = canBeDelivered;
            StoreId = store.Id;
            Store = store;
        }

        public int Id { get; private set; }
        public string Name { get; set; } = default!;
        public bool CanBeDelivered { get; set; }
        public int StoreId { get; set; }
        public virtual ActiveStore Store { get; set; } = default!;
    }
}
