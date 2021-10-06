using System;
using System.Collections.Generic;
using System.Linq;

namespace ScsOnlineShop.Application.Entities
{
    public class ActiveStore : Store
    {
        private ActiveStore() { }
        public ActiveStore(Store s)
            : base(location: s.Location, floor: s.Floor,
                  companyName: s.CompanyName, state: StoreState.Active, deliveryOption: s.DeliveryOption)
        {
        }
        public override StoreState State => StoreState.Active;
        // Es ist ein JOIN in die Tabelle ProductCategory nötig.
        public virtual ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>(0);
        public virtual ICollection<BusinessHour> BusinessHours { get; } = new List<BusinessHour>(0);
        public virtual ICollection<DeliveryHour> DeliveryHours { get; } = new List<DeliveryHour>(0);
        public bool IsInBusinessTime(DateTime dateTime)
        {
            var dayOfWeek = dateTime.DayOfWeek;
            var hour = dateTime.Hour;
            return BusinessHours.Any(b => b.DayOfWeek == dayOfWeek
                && hour >= b.HourFrom
                && hour < b.HourTo);
        }
    }
}
