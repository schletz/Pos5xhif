using System.Collections.Generic;

namespace Spg_Schoolrating.Application.Domain
{
    public class SchoolRating : Rating
    {
        public int SchoolId { get; set; }
        public School School { get; set; }
        public int RatingCategoryId { get; set; }
        public SchoolRatingCategory RatingCategory { get; set; }
    }

}
