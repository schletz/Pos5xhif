using System;
using System.Collections.Generic;
using System.Text;

namespace SPG_Fachtheorie.Aufgabe1.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int SocialSecurityNummer { get; set; }
        public string Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<Test> Tests { get; set; }
    }
}
