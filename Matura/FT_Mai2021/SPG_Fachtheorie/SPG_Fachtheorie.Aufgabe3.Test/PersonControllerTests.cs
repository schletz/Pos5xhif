using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe3.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe3.Test
{
    /// <summary>
    /// Testet die Klasse PersonController.
    /// Hinweise:
    ///     Testen Sie alle möglichen Statuscodes der Routen.
    ///     Mit nachfolgendem SQL Statement können Sie die korrekte Anzahl der Tests nach Geburtsjahr und Geschlecht herausfinden.
    ///     Sie finden die Musterdatenbank in CovidTest.db im Ordner der sln Datei.
    ///     Verwenden Sie die Methode GetContext(), um die Musterdatenbank zu erzeugen.
    /// </summary>
    /*
        SELECT
            strftime('%Y', p.BirthDate) AS BirthYear,
            SUM(CASE WHEN Gender = 0 THEN 1 ELSE 0 END) AS AnzMale,
            SUM(CASE WHEN Gender = 1 THEN 1 ELSE 0 END) AS AnzFemale,
            SUM(CASE WHEN Gender = 2 THEN 1 ELSE 0 END) AS AnzDivers
        FROM Tests t INNER JOIN Persons p ON (t.PersonId == p.Id)
        WHERE t.Result IS NOT NULL
        GROUP BY strftime('%Y', p.BirthDate);
    */
    [Collection("Sequential")]
    public class PersonControllerTests
    {
        private CovidTestContext GetContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=CovidTest.db")
                .Options;
            var db = new CovidTestContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Import("data.sql");
            return db;
        }

        /// <summary>
        /// Testet die Methode GetStatistics mit includeTestResults=no.
        /// Sie können das nachfolgende SQL Statement verwenden, um in DBeaver eine Übersicht über die
        /// Werte in der Datenbank zu erhalten.
        /// </summary>
        /*
            SELECT
	            strftime('%Y', p.BirthDate) AS BirthYear,
	            SUM(CASE WHEN Gender = 0 THEN 1 ELSE 0 END) AS AnzMale,
	            SUM(CASE WHEN Gender = 1 THEN 1 ELSE 0 END) AS AnzFemale,
	            SUM(CASE WHEN Gender = 2 THEN 1 ELSE 0 END) AS AnzDivers
            FROM Tests t INNER JOIN Persons p ON (t.PersonId == p.Id)
            WHERE t.Result IS NOT NULL
            GROUP BY strftime('%Y', p.BirthDate);
        */
        [Fact]
        public void GetStatisticsWithoutDetailsTest()
        {
            using (var db = GetContext())
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///  Testet die Methode GetStatistics mit includeTestResults=yes.
        /// </summary>
        [Fact]
        public void GetStatisticsWithDetailsTest()
        {
            using (var db = GetContext())
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Testet die Methode AddTest, ob sie erfolgreich einen Test einträgt.
        /// </summary>
        [Fact]
        public void AddTestSuccessTest()
        {
            using (var db = GetContext())
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Testet, ob AddTest NotFound bei einer ungültigen Personen ID liefert.
        /// </summary>
        [Fact]
        public void AddTestNotFoundTest()
        {
            using (var db = GetContext())
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Testet, ob AddTest BadRequest bei einem ungültigen Stationsnamen liefert.
        /// </summary>
        [Fact]
        public void AddTestBadRequestTest()
        {
            using (var db = GetContext())
            {
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Prüft, ob UpdateTest erfolgreich die Testdaten aktualisiert.
        /// </summary>
        [Fact]
        public void UpdateTestSuccessTest()
        {
            using (var db = GetContext())
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Prüft, ob UpdateTest NotFound bei einer ungültigen Test ID liefert.
        /// </summary>
        [Fact]
        public void UpdateTestNotFoundTest()
        {
            using (var db = GetContext())
            {
                throw new NotImplementedException();
            }
        }

    }
}
