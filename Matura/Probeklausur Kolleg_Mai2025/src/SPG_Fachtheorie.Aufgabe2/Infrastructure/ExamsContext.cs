using Bogus;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Infrastructure
{
    public class ExamsContext : DbContext
    {
        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Participation> Participations => Set<Participation>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<PossibleAnswer> PossibleAnswers => Set<PossibleAnswer>();

        public ExamsContext()
        { }

        public ExamsContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=exams.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            Randomizer.Seed = new Random(2204);

            var exams = new Faker<Exam>()
                .CustomInstantiator(f =>
                {
                    var failThreshold = f.Random.Int(30, 50);
                    var visible = f.Random.Bool(0.8f);
                    return new Exam(f.Lorem.Word(), failThreshold, visible);
                })
                .Generate(10)
                .ToList();
            Exams.AddRange(exams);
            SaveChanges();

            var questions = new Faker<Question>()
                .CustomInstantiator(f =>
                {
                    var exam = f.Random.ListItem(exams);
                    var question = new Question(f.Lorem.Sentence(), exam);
                    var possibleAnswers = new Faker<PossibleAnswer>()
                        .CustomInstantiator(f =>
                        {
                            var points = f.Random.Int(0, 1) * 5;
                            return new PossibleAnswer(question, f.Lorem.Sentence(), points);
                        })
                        .Generate(4)
                        .ToList();
                    question.PossibleAnswers.AddRange(possibleAnswers);
                    return question;
                })
                .Generate(30)
                .ToList();
            Questions.AddRange(questions);
            SaveChanges();

            var students = new Faker<Student>()
                .CustomInstantiator(f =>
                {
                    var exam = f.Random.ListItem(exams);
                    return new Student(f.Name.FirstName(), new DateTime(2000, 1, 1).AddDays(f.Random.Int(0, 7 * 365)));
                })
                .Generate(5)
                .ToList();
            Students.AddRange(students);
            SaveChanges();

            var participations = new Faker<Participation>()
                .CustomInstantiator(f =>
                {
                    var exam = f.Random.ListItem(exams);
                    var questionsForExam = questions.Where(q => q.Exam == exam).ToList();
                    var student = f.Random.ListItem(students);
                    var participation = new Participation(exam, student);
                    foreach (var question in questionsForExam)
                    {
                        var possibleAnswer = f.Random.ListItem(question.PossibleAnswers);
                        participation.Answers.Add(new Answer(participation, possibleAnswer));
                    }
                    return participation;
                })
                .Generate(10)
                .ToList();
            Participations.AddRange(participations);
            SaveChanges();
        }
    }
}