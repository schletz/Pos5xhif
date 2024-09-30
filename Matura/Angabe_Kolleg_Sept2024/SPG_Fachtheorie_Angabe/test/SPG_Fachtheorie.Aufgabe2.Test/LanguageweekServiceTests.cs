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
    public class LanguageweekServiceTests
    {
        /// <summary>
        /// Generates database in C:\Scratch\Aufgabe2_Test\Debug\net8.0\languageweek.db
        /// </summary>
        private LanguageweekContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=languageweek.db")
                .Options;

            var db = new LanguageweekContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        private LanguageweekContext GetSeededDbContext()
        {
            var db = GetEmptyDbContext();
            db.Seed();
            return db;
        }

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetSeededDbContext();
            db.ChangeTracker.Clear();
            Assert.True(db.Registrations.Any());
        }

        [Fact]
        public void CalculateStatisticsTest()
        {
            using var db = GetSeededDbContext();
            var service = new LanguageweekService(db);
            var statistics = service.CalculateStatistics();
            Assert.True(statistics.Count == 2);
            Assert.True(statistics[0].TeacherEmail == "williams@spengergasse.at");
            Assert.True(statistics[0].Participants == 18);
            Assert.True(statistics[0].StudentsInClass == 25);
            Assert.True(statistics[1].TeacherEmail == "johnson@spengergasse.at");
            Assert.True(statistics[1].Participants == 19);
            Assert.True(statistics[1].StudentsInClass == 19);
        }
        [Fact]
        public void AssignSupportTeacherSuccessTest()
        {
            // TODO: Remove exception and add implementation
            throw new NotImplementedException();
        }

        [Fact]
        public void AssignSupportTeacherThrowsExceptionIfTeacherIsAlreadyAssignedTest()
        {
            // TODO: Remove exception and add implementation
            throw new NotImplementedException();
        }

        [Fact]
        public void AssignSupportTeacherThrowsExceptionIfNoFemaleTeacherTest()
        {
            // TODO: Remove exception and add implementation
            throw new NotImplementedException();
        }
    }
}