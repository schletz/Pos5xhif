using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.Fachtheorie.Aufgabe2.Model;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using static Bogus.DataSets.Name;

namespace Spg.Fachtheorie.Aufgabe2.Services
{
    public class Aufgabe2Database : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Grade> Grades => Set<Grade>();

        public Aufgabe2Database()
        { }
        public Aufgabe2Database(DbContextOptions options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Subject>()
                .HasOne(e => e.Grade)
                .WithOne(e => e.SubjectNavigation)
                .HasForeignKey<Grade>(e => e.SubjectId)
                .IsRequired();
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1832);

            var students = new Faker<Student>("de").CustomInstantiator(f =>
            {
                var lastname = f.Name.LastName();
                var firstname = f.Name.FirstName();
                return new Student() { FirstName = firstname, LastName = lastname };
            })
            .Generate(20)
            .ToList();
            Students.AddRange(students);
            SaveChanges();

            Faker f = new Faker();
            var subjects = new List<Subject>()
            {
                new Subject() { 
                    Name = "AM", 
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "POS", 
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "DBI", 
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "E",
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "D",
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "PRE",
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "BWM",
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "WMC",
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "TINF",
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "BAP",
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "IOT",
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
                new Subject() {
                    Name = "SOS",
                    Seats = f.Random.Int(1, 20), StudentNavigation = f.Random.ListItem(students) },
            };
            Subjects.AddRange(subjects);
            SaveChanges();

            var grades = new Faker<Grade>("de").CustomInstantiator(f =>
            {
                return new Grade() { Value = f.Random.Int(1, 5), SubjectNavigation = f.Random.ListItem(subjects) };
            })
            .Generate(50)
            .ToList();
            Grades.AddRange(grades);
            SaveChanges();

        }
    }
}