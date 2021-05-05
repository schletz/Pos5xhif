using System.Collections.Generic;

namespace Spg_Schoolrating.Application.Domain
{
    public class Teacher
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public string Email { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
        public List<TeacherRating> TeacherRatings { get; set; }
    }

}
