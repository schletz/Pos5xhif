using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SPG_Fachtheorie.Aufgabe2;
using Microsoft.EntityFrameworkCore;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    [Collection("Sequential")]
    public class GradeServiceTests
    {
        /// <summary>
        /// Legt die Datenbank an und befüllt sie mit Musterdaten. Die Datenbank ist
        /// nach Ausführen des Tests ServiceClassSuccessTest in
        /// C:\Scratch\Aufgabe2_Test\bin\Debug\net6.0\Grades.db
        /// und kann mit SQLite Manager, DBeaver, ... betrachtet werden.
        /// </summary>
        private GradeContext GetContext(bool deleteDb = true)
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Grades.db")
                .Options;

            var db = new GradeContext(options);
            if (deleteDb)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Seed();
            }
            return db;
        }
        /// <summary>
        /// Erzeugt die Datenbank in C:\Scratch\Aufgabe2_Test\Debug\net6.0
        /// </summary>
        [Fact]
        public void ServiceClassSuccessTest()
        {
            using var db = GetContext();
            Assert.True(db.Students.Count() > 0);
            Assert.True(db.Students.Include(s => s.Grades).First().Grades.Count() > 0);
            var service = new GradeService(db);
        }

        [Fact]
        public void TryAddRegistrationReturnsFalseIfSubjectDoesNotExist()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void TryAddRegistrationReturnsFalseIfSubjectIsNotNegative()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void TryAddRegistrationReturnsFalseIfExamExists()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void TryAddRegistrationReturnsFalseOnDateConflict()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void TryAddRegistrationReturnsSuccessTest()
        {
            throw new NotImplementedException();
        }
    }
}
