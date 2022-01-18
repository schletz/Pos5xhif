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
    public class AppointmentServiceTests
    {
        /// <summary>
        /// Legt die Datenbank an und befüllt sie mit Musterdaten. Die Datenbank ist
        /// nach Ausführen des Tests ServiceClassSuccessTest in
        /// SPG_Fachtheorie\SPG_Fachtheorie.Aufgabe2.Test\bin\Debug\net6.0\Appointment.db
        /// und kann mit SQLite Manager, DBeaver, ... betrachtet werden.
        /// </summary>
        private AppointmentContext GetAppointmentContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Appointment.db")
                .Options;

            var db = new AppointmentContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();
            return db;
        }
        [Fact]
        public void ServiceClassSuccessTest()
        {
            using var db = GetAppointmentContext();
            Assert.True(db.Students.Count() > 0);
            var service = new AppointmentService(db);
        }
        [Fact]
        public void AskForAppointmentSuccessTest()
        {

        }
        [Fact]
        public void AskForAppointmentReturnsFalseIfNoOfferExists()
        {

        }
        [Fact]
        public void AskForAppointmentReturnsFalseIfOutOfDate()
        {

        }
        [Fact]
        public void ConfirmAppointmentSuccessTest()
        {

        }
        [Fact]
        public void ConfirmAppointmentReturnsFalseIfStateIsInvalid()
        {

        }
        [Fact]
        public void CancelAppointmentStudentSuccessTest()
        {

        }
        [Fact]
        public void CancelAppointmentCoachSuccessTest()
        {

        }
        [Fact]
        public void ConfirmAppointmentStudentReturnsFalseIfStateIsInvalid()
        {

        }
        [Fact]
        public void ConfirmAppointmentCoachReturnsFalseIfStateIsInvalid()
        {

        }
    }
}
