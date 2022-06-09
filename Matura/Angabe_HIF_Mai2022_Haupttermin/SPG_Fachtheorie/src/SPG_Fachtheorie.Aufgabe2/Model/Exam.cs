using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public class Exam
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int? ExamResult { get; set; }
        public int? NewGradeValue { get; set; }
        public Guid GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
