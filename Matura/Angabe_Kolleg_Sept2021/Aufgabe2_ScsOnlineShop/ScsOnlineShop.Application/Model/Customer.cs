namespace ScsOnlineShop.Application.Model
{
    public class Customer
    {
        public Customer(string firstname, string lastname, string email, Address address)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Address = address;
        }
        protected Customer() { }   // EF Core Proxy
        public int Id { get; private set; }
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Address Address { get; set; } = default!;
    }
}
