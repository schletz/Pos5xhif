using System;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public class Student 
    { 
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
