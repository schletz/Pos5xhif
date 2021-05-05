using System.Collections.Generic;
using System.Text;

namespace Spg_Schoolrating.Application.Domain
{
    public class School
    {
        public int Id { get; set; }
        public int SchoolNumber { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public SchoolType SchoolType { get; set; }
        public int? PupilsCount { get; set; }
        public List<SchoolRating> SchoolRatings { get; set; }
    }

}
