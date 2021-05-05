using System;

namespace Spg_Schoolrating.Application.Domain
{
    public class Rating
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime RatingDate { get; set; }
        public DateTime? RatingUpdated { get; set; }
    }

}
