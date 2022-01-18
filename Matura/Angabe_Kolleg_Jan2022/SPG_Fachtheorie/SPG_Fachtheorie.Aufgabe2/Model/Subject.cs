using System;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public class Subject 
    { 
        public Guid Id { get; set; }
        public int Term { get; set; }
        public string Name { get; set; }
        public string EducationType { get; set; }
    }
}
