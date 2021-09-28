using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Application.Entities
{
    public class Store
    {
        public Store(string location, string floor, string companyName, StoreState state)
        {
            Location = location;
            Floor = floor;
            CompanyName = companyName;
            State = state;
        }
        protected Store() { }  // For ef core
        public int Id { get; private set; }  // int -> AutoIncrement, Id -> PK
        public string Location { get; set; } = default!;
        public string Floor { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public StoreState State { get; set; }
    }
}
