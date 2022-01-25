using SPG_Fachtheorie.Aufgabe1.Domain;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public class CovidTestService
    {
        private readonly CovidTestContext _db;

        public CovidTestService(CovidTestContext db)
        {
            _db = db;
        }

        /// <summary>
        /// DTO Klasse für die Ausgabe der Teststatistik
        /// </summary>
        public class TestStatistics
        {
            public Gender Gender { get; set; }
            public int AgeCategory { get; set; }
            public int Count { get; set; }
            public decimal CountRelative { get; set; }
        }
        /// <summary>
        /// Berechnet die Teststatistik lt. Aufgabe 2c.
        /// Hinweise:
        ///    Das Alter können Sie zur Vereinfachung mit Stichtag 1.1.2021 berechnen (Alter = 2021 - Geburtsjahr)
        ///    Die Kategorien können numerisch ausgegeben werden:
        ///        0: <10, 1: <20, 2: <30, 3: <40, 4: <50, 5: <60, 6: >60
        ///    Die relative Anzahl ist als Prozentwert (0 - 100) relativ zu den gesamten Tests in der Testtabelle auszugeben.
        ///    Es ist keine Rundung vorzunehmen, der Prozentwert kann in voller Genauigkeit zurückgegeben werden.
        /// </summary>
        /// <returns></returns>
        public List<TestStatistics> GetTestStatistics()
        {
            return _db.Tests
                .GroupBy(t => new { t.Person.Gender, AgeCategory = Math.Min(6, (2021 - t.Person.BirthDate.Year) / 10) })
                .Select(g => new TestStatistics
                {
                    Gender = g.Key.Gender,
                    AgeCategory = g.Key.AgeCategory,
                    Count = g.Count(),
                    CountRelative = g.Count() * 100M / _db.Tests.Count()
                })
                .ToList();

        }
    }
}
