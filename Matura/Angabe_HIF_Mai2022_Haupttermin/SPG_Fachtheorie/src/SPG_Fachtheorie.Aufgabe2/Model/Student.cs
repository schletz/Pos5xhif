using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
        public bool? ConferenceDecision { get; set; }
        public Guid Guid => Id;
        public List<Grade> Grades { get; }
    }
}
