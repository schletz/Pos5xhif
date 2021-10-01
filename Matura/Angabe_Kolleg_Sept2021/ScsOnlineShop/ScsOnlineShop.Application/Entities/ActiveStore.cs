namespace ScsOnlineShop.Application.Entities
{
    public class ActiveStore : Store
    {
        public string OpeningHours { get; set; } = default!;
        private ActiveStore() { }
        public ActiveStore(string openingHours, Store s) 
            : base(location: s.Location, floor: s.Floor, 
                  companyName: s.CompanyName, state: StoreState.Active, deliveryOption: s.DeliveryOption)
        {
            OpeningHours = openingHours;
        }
    }
}
