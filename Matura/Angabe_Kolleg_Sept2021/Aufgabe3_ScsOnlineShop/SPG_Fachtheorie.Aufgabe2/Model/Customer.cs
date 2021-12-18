using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    [Table("Customer")]
    public class Customer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
