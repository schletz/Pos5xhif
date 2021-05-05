using Spg_Schoolrating.Application.Domain;
using Spg_Schoolrating.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spg_Schoolrating.Application.Services
{
    public class RatingService
    {
        private readonly RatingContext _db;

        public class RatingDto
        {
            public int CategoryId { get; set; }
            public int Value { get; set; }
        }

        public class SchoolStat
        {
            public class Category
            {
                public string Name { get; set; }
                public decimal AvgValue { get; set; }
            }
            public int SchoolNumber { get; set; }
            public List<Category> Categories { get; set; }
        }

        public RatingService(RatingContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Fügt die übergebenen Ratings eines Lehrers ein.
        /// </summary>
        /// <param name="ratingDtos"></param>
        public void InsertTeacherRating(int teacherId, IEnumerable<RatingDto> ratingDtos)
        {
            var ratings = ratingDtos.Select(r => new TeacherRating
            {
                RatingCategoryId = r.CategoryId,
                TeacherId = teacherId,
                RatingDate = DateTime.UtcNow
            });
            _db.TeacherRatings.AddRange(ratings);
            _db.SaveChanges();
        }

        /// <summary>
        /// Fügt die übergebenen Ratings eines Lehrers ein.
        /// </summary>
        /// <param name="ratingDtos"></param>
        public void InsertSchoolRating(int schoolId, IEnumerable<RatingDto> ratingDtos)
        {
            var ratings = ratingDtos.Select(r => new SchoolRating
            {
                RatingCategoryId = r.CategoryId,
                SchoolId = schoolId,
                RatingDate = DateTime.UtcNow
            });
            _db.SchoolRatings.AddRange(ratings);
            _db.SaveChanges();
        }


        /// <summary>
        /// Liefert die durchschnittlichen Bewertungen der einzelnen Kategorien für eine Schule.
        /// </summary>
        public SchoolStat GetSchoolStatistics(int schoolId)
        {
            return _db.Schools
                .Where(s => s.Id == schoolId)
                .Select(s => new SchoolStat
                {
                    SchoolNumber = s.SchoolNumber,
                    Categories = s.SchoolRatings
                        .GroupBy(r => r.RatingCategory.Name)
                        .Select(g => new SchoolStat.Category
                        {
                            Name = g.Key,
                            AvgValue = (decimal)g.Average(ra => ra.Value)
                        })
                        .ToList()
                })
                .FirstOrDefault();
        }


    }
}
