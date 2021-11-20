namespace ScsOnlineShop.Application.Model
{
    public class Store
    {
        public Store(string name)
        {
            Name = name;
        }

        protected Store() { }
        public int Id { get; private set; }
        public string Name { get; set; } = default!;
    }
}
