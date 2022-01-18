using Bogus;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext(DbContextOptions opt) : base(opt) { }

        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Offer> Offers => Set<Offer>();
        public DbSet<Subject> Subjects => Set<Subject>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1742);
            var faker = new Faker("de");

            var subjects = new Subject[]
            {
                new Subject{Id = faker.Random.Guid(), Term = 3, Name = "POS", EducationType = "BIF"},
                new Subject{Id = faker.Random.Guid(), Term = 3, Name = "DBI", EducationType = "BIF"},
                new Subject{Id = faker.Random.Guid(), Term = 5, Name = "DBI", EducationType = "KIF"},
            };
            Subjects.AddRange(subjects);
            SaveChanges();

            var students = new Faker<Student>("de").CustomInstantiator(f =>
            {
                var lastname = f.Name.LastName();
                var username = (lastname.Length < 3 ? lastname : lastname.Substring(0, 3)).ToLower()
                        + f.Random.Int(100000, 999999).ToString();
                return new Student
                {
                    Id = f.Random.Guid(),
                    Firstname = f.Name.FirstName(),
                    Lastname = lastname,
                    Email = username.ToLower() + "@spengergasse.at",
                    Username = username.ToUpper()
                };
            })
            .Generate(20)
            .GroupBy(s => s.Username).Select(g => g.First())
            .ToList();

            var coaches = new Faker<Coach>("de").CustomInstantiator(f =>
            {
                var lastname = f.Name.LastName();
                var username = (lastname.Length < 3 ? lastname : lastname.Substring(0, 3)).ToLower()
                        + f.Random.Int(100000, 999999).ToString();
                return new Coach
                {
                    Id = f.Random.Guid(),
                    Firstname = f.Name.FirstName(),
                    Lastname = lastname,
                    Email = username.ToLower() + "@spengergasse.at",
                    Username = username.ToUpper(),
                    Phone = f.Random.Int(100000, 999999).ToString()
                };
            })
            .Generate(5)
            .GroupBy(s => s.Username).Select(g => g.First())
            .Where(c => !students.Any(s => s.Username == c.Username))
            .ToList();
            Students.AddRange(students);
            Students.AddRange(coaches);
            SaveChanges();

            var offers = new Faker<Offer>("de").CustomInstantiator(f =>
            {
                var from = new DateTime(2021, 9, 1).AddDays(f.Random.Int(0, 120));
                return new Offer
                {
                    Id = f.Random.Guid(),
                    Teacher = f.Random.ListItem(coaches),
                    Subject = f.Random.ListItem(subjects),
                    From = from,
                    To = from.AddDays(f.Random.Int(30, 120))
                };
            })
            .Generate(20)
            .GroupBy(o => new { o.Teacher.Id, SId = o.Subject.Id }).Select(g => g.First())
            .ToList();
            Offers.AddRange(offers);
            SaveChanges();

            var locations = new string[] { "C3.07", "C3.08", "C3.09", "C3.10", "C3.11" };
            var appointments = new Faker<Appointment>("de").CustomInstantiator(f =>
            {
                var offer = f.Random.ListItem(offers);
                return new Appointment
                {
                    Id = f.Random.Guid(),
                    Offer = offer,
                    Student = f.Random.ListItem(students),
                    Date = offer.From.AddDays(f.Random.Int(0, (int)(offer.To - offer.From).TotalDays)),
                    Location = f.Random.ListItem(locations).OrDefault(f, 0.5f),
                    State = f.Random.Enum<AppointmentState>()
                };
            })
            .Generate(40)
            .ToList();
            Appointments.AddRange(appointments);
            SaveChanges();
        }
    }
}
