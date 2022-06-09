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
    public class GradeContext : DbContext
    {
        public GradeContext(DbContextOptions opt) : base(opt) { }

        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Class> Classes => Set<Class>();
        public DbSet<Teacher> Teachers => Set<Teacher>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>().OwnsOne(t => t.Subject);
            modelBuilder.Entity<Class>().HasIndex(c => c.Name).IsUnique();
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1000);
            var faker = new Faker("de");
            var teachers = new Faker<Teacher>("de").CustomInstantiator(f =>
                {
                    var lastname = f.Name.LastName();
                    return new Teacher
                    {
                        Lastname = lastname,
                        Firstname = f.Name.FirstName(),
                        Email = lastname.ToLower() + "@spengergasse.at"
                    };
                })
                .Generate(20)
                .ToList();
            Teachers.AddRange(teachers);
            SaveChanges();

            var classes = new Class[]
            {
                new Class{Name = "3AHIF"},
                new Class{Name = "3BHIF"},
                new Class{Name = "3CHIF"},
                new Class{Name = "4AHIF"},
                new Class{Name = "4BHIF"},
                new Class{Name = "4CHIF"}
            };
            Classes.AddRange(classes);
            SaveChanges();

            var subjects = new Subject[]
            {
                new Subject{Shortname = "POS", Longmame = "Programmieren" },
                new Subject{Shortname = "DBI", Longmame = "Datenbanken" },
                new Subject{Shortname = "D", Longmame = "Deutsch" },
                new Subject{Shortname = "E", Longmame = "Englisch" },
                new Subject{Shortname = "AM", Longmame = "Angewandte Mathematik" }
            };

            // Für jeden Gegenstand und jede Klasse eine Lesson mit zufälligem
            // Teacher generieren.
            var lessons = new List<Lesson>();
            foreach (var c in classes)
            foreach (var s in subjects)
            {
                    var teacher = faker.Random.ListItem(teachers);
                    lessons.Add(new Lesson
                    {
                        Class = c,
                        ClassId = c.Id,
                        Subject = new Subject { Shortname = s.Shortname, Longmame = s.Longmame },
                        Teacher = teacher,
                        TeacherId = teacher.Id
                    });
            }
            Lessons.AddRange(lessons);
            SaveChanges();

            var students = new Faker<Student>("de").CustomInstantiator(f =>
            {
                var lastname = f.Name.LastName();
                   
                return new Student
                {
                    Firstname = f.Name.FirstName(),
                    Lastname = lastname,
                    Email = lastname.ToLower()+"@spengergasse.at",
                    Class = f.Random.ListItem(classes),
                };
            })
            .Generate(120)
            .GroupBy(s => s.Email)
            .Select(g => g.First())
            .ToList();
            Students.AddRange(students);
            SaveChanges();

            // Jedem Student in jeder Stunde eine Note generieren.
            var marks = new int[] { 1, 2, 3, 4, 5 };
            var markWeights = new float[] { 0.1f, 0.1f, 0.1f, 0.2f, 0.5f };
            var grades = new List<Grade>();
            foreach (var s in students)
                foreach(var l in lessons)
                {
                    if (l.ClassId != s.ClassId) { continue; }
                    grades.Add(new Grade
                    {
                        Student = s,
                        StudentId = s.Id,
                        Lesson = l,
                        LessonId = l.Id,
                        GradeValue = faker.Random.WeightedRandom(marks, markWeights)
                    });
                }
            Grades.AddRange(grades);
            SaveChanges();

        }
    }
}
