using System.Collections.Generic;

namespace Spg_Hotelmanager.Application.Domain
{
    public class Customer : Person
    {
        public string PassportNumber { get; set; }
        public Address HomeAddress { get; set; }
        public Address BillingAddress { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
