using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe2.Services;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    [Collection("Sequential")]
    public class ExamServiceTests
    {
        /// <summary>
        /// Generates database in C:\Scratch\Aufgabe2_Test\Debug\net8.0\exams.db
        /// </summary>
        private ExamsContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("DataSource=exams.db")
                .Options;
            var db = new ExamsContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void CreateDatabaseSuccessTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            Assert.True(db.Exams.Count() > 0);
        }

        /// <summary>
        /// AddQuestionSuccessTest prüft, ob die Methode eine neue Frage zu einer Prüfung (Exam) speichert,
        /// wenn alle Bedingungen eingehalten werden.
        /// </summary>
        [Fact]
        public void AddQuestionSuccessTest()
        {

        }

        /// <summary>
        /// AddQuestionThorowsArgumentExceptionWhenExamIdIsInvalidTest prüft, ob die Methode eine ArgumentException wirft,
        /// wenn die übergebene examId nicht gefunden wird.
        /// </summary>
        [Fact]
        public void AddQuestionThorowsArgumentExceptionWhenExamIdIsInvalidTest()
        {

        }

        /// <summary>
        /// AddQuestionThorowsExamServiceExceptionWhenQuestionNumbersIsInvalidTest prüft, ob die Methode eine
        /// ExamServiceException mit dem Text „Exam already has 5 questions“ wirft, wenn bei einer Prüfung (Exam),
        /// die bereits 5 oder mehr Fragen hat, eine Frage eingefügt werden soll.
        /// </summary>
        [Fact]
        public void AddQuestionThorowsExamServiceExceptionWhenQuestionNumbersIsInvalidTest()
        {

        }

        /// <summary>
        /// Testet die Methode CalculateExamResults im ExamService.
        /// </summary>
        [Fact]
        public void CalculateExamStatisitcsSuccessTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            var service = new ExamService(db);
            var results = service.CalculateExamResults();
            Assert.True(results.Count() == 10);
            Assert.True(results.Exists(e => e.ExamName == "nam" && e.StudentName == "Melyssa" && e.Points == 10));
            Assert.True(results.Exists(e => e.ExamName == "rem" && e.StudentName == "Rachael" && e.Points == 5));
            Assert.True(results.Exists(e => e.ExamName == "doloribus" && e.StudentName == "Rachael" && e.Points == 15));
            Assert.True(results.Exists(e => e.ExamName == "explicabo" && e.StudentName == "Rachael" && e.Points == 10));
            Assert.True(results.Exists(e => e.ExamName == "doloribus" && e.StudentName == "Reynold" && e.Points == 15));
            Assert.True(results.Exists(e => e.ExamName == "laboriosam" && e.StudentName == "Keeley" && e.Points == 15));
            Assert.True(results.Exists(e => e.ExamName == "est" && e.StudentName == "Aurelio" && e.Points == 5));
            Assert.True(results.Exists(e => e.ExamName == "quisquam" && e.StudentName == "Keeley" && e.Points == 5));
            Assert.True(results.Exists(e => e.ExamName == "est" && e.StudentName == "Keeley" && e.Points == 10));
            Assert.True(results.Exists(e => e.ExamName == "nam" && e.StudentName == "Aurelio" && e.Points == 10));
        }
    }
}