namespace Spg_Schoolrating.Application.Domain
{
    public class TeacherRating : Rating
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int RatingCategoryId { get; set; }
        public TeacherRatingCategory RatingCategory { get; set; }
    }

}
