using System.Collections.Generic;

namespace ScsOnlineShop.Application.Entities
{
    public class Tenant
    {
        public Tenant(string firstname, string lastname, string email)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
        }
        private Tenant() { }
        // AutoIncrement ID -> private set
        public int Id { get; private set; }
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public virtual ICollection<Store> Stores { get; } = new List<Store>(0);
    }
}
