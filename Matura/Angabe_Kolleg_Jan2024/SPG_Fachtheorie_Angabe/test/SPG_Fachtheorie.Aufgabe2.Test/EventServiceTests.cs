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
    public class EventServiceTests
    {
        /// <summary>
        /// Generates database in C:\Scratch\Aufgabe2_Test\Debug\net6.0\event.db
        /// </summary>
        private EventContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=event.db")
                .Options;

            var db = new EventContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            db.ChangeTracker.Clear();
            Assert.True(db.Tickets.Any());
        }

        [Fact]
        public void ShouldThrowException_WhenInvalidGuestId()
        {
        }

        [Fact]
        public void ShouldThrowException_WhenNoTickets()
        {
        }

        [Fact]
        public void ShouldThrowException_WhenGuestHasTicketForShowAndContingentReserved()
        {
        }

        [Fact]
        public void ShouldThrowException_WhenShowDateNot14DaysInFuture()
        {
        }

        [Fact]
        public void ShouldReturnTicketId_WhenParametersAreValid()
        {
        }

        /// <summary>
        /// SELECT t.TicketState, SUM(t.Pax+1) AS Count
        /// FROM Tickets t
        /// WHERE t.ContingentId = 68
        /// GROUP BY t.TicketState;
        /// </summary>
        [Fact]
        public void CalcContingentStatisticsTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            var service = new EventService(db);
            var statistics = service.CalcContingentStatistics(68);
            Assert.True(statistics.ReservedTickets == 23);
            Assert.True(statistics.SoldTickets == 17);
        }
    }
}