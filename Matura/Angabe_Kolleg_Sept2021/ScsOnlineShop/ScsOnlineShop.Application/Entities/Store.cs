using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Application.Entities
{
    public enum DeliveryOption { Postal = 1, OwnShipping, ClickCollect}
    public class Store
    {
        // Alle Felder, die NOT NULL sind, werden hier gesetzt.
        public Store(string location, string floor, string companyName, StoreState state, DeliveryOption deliveryOption)
        {
            Location = location;
            Floor = floor;
            CompanyName = companyName;
            State = state;
            DeliveryOption = deliveryOption;
        }
        protected Store() { }                // For ef core
        public int Id { get; private set; }  // int -> AutoIncrement, Id -> PK
        public string Location { get; set; } = default!;
        public string Floor { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        // Wert des FK
        public int? TenantId { get; set; }
        // Navigation zum FK Objekt
        public virtual Tenant? Tenant { get; set; }
        public virtual StoreState State { get; set; }
        // Besser wäre eine lookup table
        public DeliveryOption DeliveryOption { get; set; }
    }
}
