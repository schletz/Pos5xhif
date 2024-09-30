using Bogus;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Infrastructure
{
    public class LanguageweekContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Schoolclass> Schoolclasses => Set<Schoolclass>();
        public DbSet<Languageweek> Languageweeks => Set<Languageweek>();
        public DbSet<Registration> Registrations => Set<Registration>();
        public DbSet<Destination> Destinations => Set<Destination>();

        public LanguageweekContext()
        { }

        public LanguageweekContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=languageweek.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Languageweek>().HasOne(lw => lw.Teacher).WithMany(t => t.LanguageweekTeachers);
            modelBuilder.Entity<Languageweek>().HasOne(lw => lw.SupportTeacher).WithMany(t => t.LanguageweekSupportTeachers);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Generic config for all entities
                // ON DELETE RESTRICT instead of ON DELETE CASCADE
                foreach (var key in entityType.GetForeignKeys())
                    key.DeleteBehavior = DeleteBehavior.Restrict;

                foreach (var prop in entityType.GetDeclaredProperties())
                {
                    // Define Guid as alternate key. The database can create a guid fou you.
                    if (prop.Name == "Guid")
                    {
                        modelBuilder.Entity(entityType.ClrType).HasAlternateKey("Guid");
                        prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                    }
                    // Default MaxLength of string Properties is 255.
                    if (prop.ClrType == typeof(string) && prop.GetMaxLength() is null) prop.SetMaxLength(255);
                    // Seconds with 3 fractional digits.
                    if (prop.ClrType == typeof(DateTime)) prop.SetPrecision(3);
                    if (prop.ClrType == typeof(DateTime?)) prop.SetPrecision(3);
                }
            }
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1832);
            var minDate = new DateTime(2023, 6, 1);
            var maxDate = new DateTime(2024, 6, 1);
            var faker = new Faker();

            // Generate a list of 5 Teachers without Bogus
            var teachers = new List<Teacher>
            {
                new Teacher("WIS", "Stefanie", "Williams", "williams@spengergasse.at", Gender.Female),
                new Teacher("JOS", "Susan", "Johnson", "johnson@spengergasse.at", Gender.Female),
                new Teacher("BRM", "Michael", "Brown", "brown@spengergasse.at", Gender.Male),
                new Teacher("SMM", "Martin", "Smith", "smith@spengergasse.at", Gender.Male),
                new Teacher("JOM", "Manfred", "Jones", "jones@spengergasse.at", Gender.Male),
            };
            Teachers.AddRange(teachers);
            SaveChanges();

            var schoolclasses = new List<Schoolclass>
            {
                new Schoolclass("4AHIF", "HIF"),
                new Schoolclass("4BHIF", "HIF"),
                new Schoolclass("4CHIF", "HIF"),
                new Schoolclass("4AHBGM", "HBGM")
            };
            Schoolclasses.AddRange(schoolclasses);
            SaveChanges();

            var destinations = new List<Destination>
            {
                new Destination("London", "Großbritannien"),
                new Destination("Dublin", "Irland"),
                new Destination("Valetta", "Malta"),
                new Destination("Edinburgh", "Großbritannien"),
                new Destination("Galway", "Irland")
            };
            Destinations.AddRange(destinations);
            SaveChanges();

            var onlyMaleClasses = new Dictionary<string, bool>
            {
                { "4AHIF", false },
                { "4BHIF", true },
                { "4CHIF", true },
                { "4AHBGM", false }
            };

            var students = new Faker<Student>("de").CustomInstantiator(f =>
            {
                var schoolclass = f.Random.ListItem(schoolclasses);
                var onlyMaleClass = onlyMaleClasses[schoolclass.Shortname];
                var gender = onlyMaleClass ? Gender.Male : f.Random.Enum<Gender>();
                var lastname = f.Name.LastName();
                var firstname = f.Name.FirstName(gender == Gender.Male ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female);
                var email = $"{firstname.ToLower()}.{lastname.ToLower()}@spengergasse.at";
                return new Student(
                    firstname, lastname, email, schoolclass,
                    gender,
                    f.Date.Between(new DateTime(2006, 1, 1), new DateTime(2007, 1, 1)).Date);
            })
            .Generate(4 * 25)
            .DistinctBy(s => s.Email)
            .ToList();
            Students.AddRange(students);
            SaveChanges();

            {
                var languageweek = new Languageweek(
                    schoolclasses.First(s => s.Shortname == "4AHIF"),
                    destinations[0],
                    new DateTime(2024, 5, 1), new DateTime(2024, 5, 9),
                    teachers[0],
                    800);
                languageweek.SupportTeacher = teachers[2];
                languageweek.Registrations.AddRange(GenerateRegistrations(faker, students, languageweek));
                Languageweeks.Add(languageweek);
            }
            {
                var languageweek = new Languageweek(
                    schoolclasses.First(s => s.Shortname == "4CHIF"),
                    destinations[0],
                    new DateTime(2024, 4, 8), new DateTime(2024, 4, 15),
                    teachers[1],
                    1200);
                languageweek.SupportTeacher = teachers[3];
                languageweek.Registrations.AddRange(GenerateRegistrations(faker, students, languageweek));
                Languageweeks.Add(languageweek);
            }
            SaveChanges();
        }

        private List<Registration> GenerateRegistrations(Faker f, List<Student> students, Languageweek languageweek)
        {
            var className = languageweek.Schoolclass.Shortname;
            var classStudents = students.Where(s => s.Schoolclass.Shortname == className).ToList();
            var count = f.Random.Int((int)Math.Ceiling(classStudents.Count * 0.7), classStudents.Count);
            var registrations = classStudents.Select(cs =>
            {
                var registrationDate = f.Date.Between(languageweek.From.AddDays(-60), languageweek.From.AddDays(-30));
                registrationDate = new DateTime(registrationDate.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond);
                return new Registration(languageweek, cs, registrationDate);
            }).ToList();

            return f.Random.ListItems(registrations, count).ToList();
        }
    }
}