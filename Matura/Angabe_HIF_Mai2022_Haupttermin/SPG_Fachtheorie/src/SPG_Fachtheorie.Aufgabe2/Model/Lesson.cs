using System;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public Subject Subject { get; set; }
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
