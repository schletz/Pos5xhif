using Bogus;
using Bogus.Extensions;
using Bogus.Distributions.Gaussian;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spg_Schoolrating.Application.Domain;

namespace Spg_Schoolrating.Application.Infrastructure
{
    public class RatingContext : DbContext
    {
        public RatingContext(DbContextOptions options) : base(options) { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RatingCategory> RatingCategories { get; set; }
        public DbSet<SchoolRating> SchoolRatings { get; set; }
        public DbSet<SchoolRatingCategory> SchoolRatingCategories { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherRating> TeacherRatings { get; set; }
        public DbSet<TeacherRatingCategory> TeacherRatingCategories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Rating.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>().OwnsOne(s => s.Address);
            modelBuilder.Entity<Teacher>().OwnsOne(t => t.Name);
        }

        /// <summary>
        /// Liefert die Schule mit der übergebenen Schulkennzahl (SchoolNumber). Liefert NULL, wenn
        /// keine Schule gefunden wurde.
        /// </summary>
        public School GetSchoolBySchoolNr(int schoolNumber)
            => Schools.FirstOrDefault(s => s.SchoolNumber == schoolNumber);
        /// <summary>
        /// Liefert alle Lehrer einer Schule
        /// </summary>
        public IQueryable<Teacher> GetTeachersBySchool(int schoolId)
            => Teachers.Where(t => t.SchoolId == schoolId);

        /// <summary>
        /// Liefert die durchschnittliche Bewertung eines Lehres in einer Kategorie
        /// </summary>
        public decimal GetAverageTeacherRating(int teacherId, int categoryId)
            => (decimal) TeacherRatings.Where(t=>t.TeacherId == teacherId && t.RatingCategoryId == categoryId).Average(t => t.Value);

        /// <summary>
        /// Liefert die durchschnittliche Bewertung eines Lehres in einer Kategorie. Dabei werden allerdings
        /// nur Ratings berücksichtigt, die nach ratetFrom abgegeben wurden. Ist RatingUpdated gesetzt, ist
        /// dieses Datum für den Vergleich zu verwenden. Ansonsten gilt das Rating Date.
        /// </summary>
        public decimal GetAverageTeacherRating(int teacherId, int categoryId, DateTime ratedFrom)
            => (decimal)TeacherRatings.Where(t => t.TeacherId == teacherId 
            && t.RatingCategoryId == categoryId
            && (t.RatingUpdated ?? t.RatingDate) > ratedFrom).Average(t => t.Value);


        //Method to fill the database with sample data.Can be ignored.
        public void Seed()
        {
            var teacherRatingCategories = new TeacherRatingCategory[]
            {
                new TeacherRatingCategory {Name = "Fachkompetenz"},
                new TeacherRatingCategory {Name = "Zuverlässigkeit"},
                new TeacherRatingCategory {Name = "Soziale Kompetenz"}
            };

            TeacherRatingCategories.AddRange(teacherRatingCategories);

            var schoolRatingCategories = new SchoolRatingCategory[]
            {
                new SchoolRatingCategory {Name = "Sauberkeit"},
                new SchoolRatingCategory {Name = "Organisation"},
                new SchoolRatingCategory {Name = "Mensa"},
                new SchoolRatingCategory {Name = "Technische Ausstattung"}
            };

            SchoolRatingCategories.AddRange(schoolRatingCategories);
            SaveChanges();
            Randomizer.Seed = new Random(1604);
            var schools = new Faker<School>("de").Rules((f, s) =>
            {
                s.SchoolNumber = f.Random.Int(100000, 999999);
                s.Name = f.Company.CompanyName();
                s.SchoolType = f.Random.Enum<SchoolType>();
                s.PupilsCount = f.Random.Int(200, 2000).OrNull(f, 0.5f);
                s.Address = new Address
                {
                    Street = f.Address.StreetAddress(),
                    Zip = f.Random.Int(1000, 9999).ToString(),
                    City = f.Address.City()
                };
                s.SchoolRatings = schoolRatingCategories
                    .SelectMany(src => new Faker<SchoolRating>("de").Rules((f2, sr) =>
                    {
                        sr.Value = f2.Random.Int(1, 5);
                        sr.RatingCategory = src;
                        sr.RatingDate = f2.Date.Between(new DateTime(2020, 9, 1), new DateTime(2021, 5, 1)).SecondsAccurate();
                        sr.RatingUpdated = sr.RatingDate.AddSeconds(f2.Random.Int(100, 86400)).OrNull(f2, 0.8f);
                    })
                    .Generate(20)
                    .ToList())
                .ToList();

            })
            .Generate(10)
            .GroupBy(s => s.SchoolNumber).Select(s => s.First())
            .GroupBy(s => s.Name).Select(s => s.First())
            .ToList();
            Schools.AddRange(schools);
            SaveChanges();

            var titles = new string[] { "Mag.", "Dr.", "MSc", "BSc" };
            var teachers = new Faker<Teacher>("de").Rules((f, t) =>
            {
                t.School = f.Random.ListItem(schools);
                t.Name = new Name
                {
                    Firstname = f.Name.FirstName(),
                    Lastname = f.Name.LastName(),
                    Title = (t.School.SchoolType == SchoolType.VS ? "BEd" : f.Random.ListItem(titles)).OrDefault(f, 0.2f)
                };
                t.Email = t.Name.Lastname.ToLower() + "@" + t.School.Name.ToLower().Replace(" ", "") + ".at";
                t.TeacherRatings = teacherRatingCategories
                    .SelectMany(trc =>
                    {
                        return new Faker<TeacherRating>("de").Rules((f2, tr) =>
                            {
                                //tr.Value = Math.Max(0, Math.Min(5, f2.Random.GaussianInt(3, 1)));
                                tr.Value = f2.Random.Int(1, 5);
                                tr.RatingCategory = trc;
                                tr.RatingDate = f2.Date.Between(new DateTime(2020, 9, 1), new DateTime(2021, 5, 1)).SecondsAccurate();
                                tr.RatingUpdated = tr.RatingDate.AddSeconds(f2.Random.Int(100, 86400)).OrNull(f2, 0.8f);
                            })
                            .Generate(5)
                            .ToList();
                    })
                    .ToList();
            })
            .Generate(100)
            .GroupBy(t => t.Email).Select(t => t.First())
            .ToList();
            Teachers.AddRange(teachers);
            SaveChanges();
        }
    }
}
