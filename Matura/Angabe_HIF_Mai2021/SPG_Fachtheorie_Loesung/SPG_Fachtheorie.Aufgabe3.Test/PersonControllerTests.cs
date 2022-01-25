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
        /// Testet die Methode GetStatistics mit gesetztem includeTestResults Parameter.
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
                var controller = new PersonController(db);
                var result = controller.GetStatistics("no") as OkObjectResult;
                Assert.NotNull(result);

                var statistics = result.Value as IEnumerable<PersonController.YearStatistics>;
                Assert.NotNull(statistics);
                var year = statistics.SingleOrDefault(s => s.YearOfBirth == 1974);
                Assert.True(year.Male == 4 && year.Female == 0 && year.Divers == 2);
            }
        }

        /// <summary>
        ///  Testet die Methode GetStatistics ohne gesetztem includeTestResults Parameter.
        /// </summary>

        [Fact]
        public void GetStatisticsWithDetailsTest()
        {
            using (var db = GetContext())
            {
                var controller = new PersonController(db);
                var result = controller.GetStatistics("no") as OkObjectResult;
                Assert.NotNull(result);

                var statistics = result.Value as IEnumerable<PersonController.YearStatistics>;
                Assert.NotNull(statistics);
                var year = statistics.SingleOrDefault(s => s.YearOfBirth == 1974);
                Assert.True(year.Male == 4 && year.Female == 0 && year.Divers == 2);
            }
        }

        [Fact]
        public void AddTestSuccessTest()
        {
            using (var db = GetContext())
            {
                var controller = new PersonController(db);
                var station = db.TestStations.First();
                var person = db.Persons.First();
                var countOld = db.Tests.Count();
                var result = controller.AddTest(person.Id, new PersonController.TestterminDto
                {
                    Station = station.StationName,
                    TimeStamp = new DateTime(2021, 6, 1, 13, 0, 0)
                }) as NoContentResult;
                Assert.NotNull(result);
                Assert.True(db.Tests.Count() == countOld + 1);
            }
        }


        [Fact]
        public void UpdateTestSuccessTest()
        {
            using (var db = GetContext())
            {
                var controller = new PersonController(db);
                var test = db.Tests.First(t=>!t.Result.HasValue);
                var result = controller.UpdateTest(test.Id, new PersonController.TestDto
                {
                    Result = TestResult.POSITIVE,
                    TestBay = 11,
                    TestKitType = TestKitType.PCR
                }) as NoContentResult;
                Assert.NotNull(result);
                db.Entry(test).Reload();
                Assert.True(test.Result == TestResult.POSITIVE);
            }
        }

    }
}
