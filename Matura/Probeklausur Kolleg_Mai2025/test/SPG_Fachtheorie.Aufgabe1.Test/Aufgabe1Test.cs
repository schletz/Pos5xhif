using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    //[Collection("Sequential")]
    public class Aufgabe1Test
    {
        private CourseContext GetEmptyDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            var db = new CourseContext(options);
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
        }

        /// <summary>
        /// Der Test AddSpeakerSuccessTest beweist, dass Sie einen Referenten (Speaker) in die Datenbank einf�gen k�nnen.
        /// Pr�fen Sie im Assert, ob ein Prim�rschl�ssel generiert wurde.
        /// </summary>
        [Fact]
        public void AddSpeakerSuccessTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Der Test AddSubscriptionSuccessTest beweist, dass Sie einen Kurs samt Anmeldung (Subscription) anlegen k�nnen.
        /// Legen Sie hierf�r einen Speaker, einen Attendee und einen Kurs (Course) an. Stellen Sie im Assert sicher, dass f�r
        /// das gespeicherte Objekt vom Typ CourseSubscription einen Prim�rschlussel generiert wurde.
        /// </summary>
        [Fact]
        public void AddSubscriptionSuccessTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Der Test DiscriminatorHasCorrectTypeSuccessTest beweist, dass der OR Mapper das Feld UserType in User korrekt bef�llt.
        /// Legen Sie daf�r einen Datensatz vom Typ Speaker an und pr�fen Sie das Feld.
        /// </summary>
        [Fact]
        public void DiscriminatorHasCorrectTypeSuccessTest()
        {
            throw new NotImplementedException();
        }

    }
}