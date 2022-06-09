using System;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    /// <summary>
    /// Die erhaltene Zeugnisnote für einen Gegenstand
    /// </summary>
    public class Grade
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int GradeValue { get; set; }
    }
}
