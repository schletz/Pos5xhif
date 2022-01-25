using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Domain;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe2.Test
{

    /// <summary>
    /// Testet die Klasse CovidTestService.
    /// Hinweise:
    ///     Mit nachfolgendem SQL Statement können Sie die korrekte Anzahl der Tests nach Altersklasse und Geschlecht herausfinden.
    ///     Sie finden die Musterdatenbank in CovidTest.db im Ordner der sln Datei.
    ///     Verwenden Sie die Methode GetContext(), um die Musterdatenbank für das Service zu erzeugen.
    /// </summary>
    /// 

    /*
	 * Gender: 0 = MALE, 1 = FEMALE, 2 = DIVERS
	 * AgeCategory: 0 = <10, 1 = <20, 2 = <30, 3 = <40, 4 = <50, 5 = <60, 6 = >60
	 
    SELECT stat.Gender, stat.AgeCategory,
	    COUNT(*) AS Anz,
	    COUNT(*) * 100.0 / (SELECT COUNT(*) FROM Tests) AS AnzRel
    FROM (
	    SELECT p.Gender,
		    MIN(6,(2021 - strftime('%Y', p.BirthDate))/10) AS AgeCategory
	    FROM Tests t INNER JOIN Persons p ON (t.PersonId = p.Id)
    ) stat
    GROUP BY stat.Gender, stat.AgeCategory
    ORDER BY Gender, AgeCategory;


	 */
    [Collection("Sequential")]
    public class CovidTestServiceTests
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
        /// Testet die Methode GetTestStatistics.
        /// </summary>
        [Fact]
        public void GetTestStatisticsSuccessTest()
        {
            using (var db = GetContext())
            {
                var service = new CovidTestService(db);
                var stat = service.GetTestStatistics();
                Assert.True(stat.Count() == 16);
            }
        }
    }

}
