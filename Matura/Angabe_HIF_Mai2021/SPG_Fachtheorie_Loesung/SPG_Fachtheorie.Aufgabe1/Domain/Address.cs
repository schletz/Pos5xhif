using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Person> Persons { get; set; }
    }

}
