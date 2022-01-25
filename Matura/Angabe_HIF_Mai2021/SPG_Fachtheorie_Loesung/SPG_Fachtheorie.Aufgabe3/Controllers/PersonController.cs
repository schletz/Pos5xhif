using SPG_Fachtheorie.Aufgabe1.Domain;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe3.Controllers
{
    /// <summary>
    /// Controller für Aufgabe 3.
    /// Als Service Implementierung (3b) gilt die Datenbank. Sie ist bereits als dll verknüpft und
    /// unabhängig von Ihrer Implementierung funktionstüchtig.
    /// </summary>
    [Route("api/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly CovidTestContext _db;

        public PersonController(CovidTestContext db)
        {
            _db = db;
        }

        public class YearStatistics
        {
            public int YearOfBirth { get; set; }
            public int Male { get; set; }
            public int Female { get; set; }
            public int Divers { get; set; }
        }

        public class YearTestStatistics
        {
            public class TestStatistics
            {
                public int Positive { get; set; }
                public int Negative { get; set; }
                public int FalsePositive { get; set; }
                public int FalseNegative { get; set; }
                public int Undefined { get; set; }
                public int Invalid { get; set; }
            }
            public int YearOfBirth { get; set; }
            public TestStatistics Male { get; set; }
            public TestStatistics Female { get; set; }
            public TestStatistics Divers { get; set; }
        }

        public class TestterminDto
        {
            public DateTime TimeStamp { get; set; }
            public string Station { get; set; }
        }

        public class TestDto
        {
            public int TestBay { get; set; }
            public TestKitType TestKitType { get; set; }
            public TestResult Result { get; set; }
        }

        [HttpGet]
        public IActionResult SayHello()
        {
            return Ok("Hello from PersonController");
        }
        /// <summary>
        /// Liefert die Statistiken über teilnehmende Personen und deren Testergebnisse
        /// Beispielaufruf: /api/persons/statistics?includeTestResults=yes
        /// Hinweise:
        ///     Es ist pro Geburtsjahr die Anzahl der Tests zu zählen (nicht der Personen).
        ///     Es sind alle Geschlechter im System (MALE, FEMALE, DIVERS) auszugeben.
        ///     Kommt ein Testergebnis nicht vor, so wird es mit der Anzahl 0 ausgegeben (es muss nicht weggelassen werden).
        ///     Ist ein Testgebnis nicht gesetzt (NULL), so ist es in allen Statistiken auszuschließen.
        /// </summary>
        [HttpGet("statistics")]
        public IActionResult GetStatistics([FromQuery] string includeTestResults)
        {
            if (includeTestResults == "no")
            {
                var result = _db.Tests
                    .Where(t => t.Result.HasValue)
                    .GroupBy(t => t.Person.BirthDate.Year)
                    .Select(g => new YearStatistics
                    {
                        YearOfBirth = g.Key,
                        Male = g.Count(t => t.Person.Gender == Gender.MALE),
                        Female = g.Count(t => t.Person.Gender == Gender.FEMALE),
                        Divers = g.Count(t => t.Person.Gender == Gender.DIVERS)
                    })
                    .ToList();
                return Ok(result);
            }
            if (includeTestResults == "yes")
            {
                var result = _db.Tests
                    .Where(t => t.Result.HasValue)
                    .GroupBy(t => t.Person.BirthDate.Year)
                    .Select(g => new YearTestStatistics
                    {
                        YearOfBirth = g.Key,
                        Male = new YearTestStatistics.TestStatistics
                        {
                            Positive = g.Count(t => t.Result == TestResult.POSITIVE),
                            Negative = g.Count(t => t.Result == TestResult.NEGATIVE),
                            FalsePositive = g.Count(t => t.Result == TestResult.FALSEPOSITIVE),
                            FalseNegative = g.Count(t => t.Result == TestResult.FALSENEGATIVE),
                            Undefined = g.Count(t => t.Result == TestResult.UNDEFINED),
                            Invalid = g.Count(t => t.Result == TestResult.INVALID)
                        },
                        Female = new YearTestStatistics.TestStatistics
                        {
                            Positive = g.Count(t => t.Result == TestResult.POSITIVE),
                            Negative = g.Count(t => t.Result == TestResult.NEGATIVE),
                            FalsePositive = g.Count(t => t.Result == TestResult.FALSEPOSITIVE),
                            FalseNegative = g.Count(t => t.Result == TestResult.FALSENEGATIVE),
                            Undefined = g.Count(t => t.Result == TestResult.UNDEFINED),
                            Invalid = g.Count(t => t.Result == TestResult.INVALID)
                        },
                        Divers = new YearTestStatistics.TestStatistics
                        {
                            Positive = g.Count(t => t.Result == TestResult.POSITIVE),
                            Negative = g.Count(t => t.Result == TestResult.NEGATIVE),
                            FalsePositive = g.Count(t => t.Result == TestResult.FALSEPOSITIVE),
                            FalseNegative = g.Count(t => t.Result == TestResult.FALSENEGATIVE),
                            Undefined = g.Count(t => t.Result == TestResult.UNDEFINED),
                            Invalid = g.Count(t => t.Result == TestResult.INVALID)
                        }
                    })
                    .ToList();
                return Ok(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// Erzeugt einen Testtermin für eine existierende Person.
        /// Hinweise:
        ///     Die ID ist die interne Personen-ID.
        ///     Erstellen Sie eine geeignete DTO Klasse für die Übergabe des Payloads.
        ///     Beim neu Anlegen eines Testtermines wird ein Test nur mit Person, Station und Timestamp angelegt.
        ///     Suchen Sie die Stations-ID durch den übergebenen Namen (er kann als eindeutig angenommen werden).
        ///     Gibt es die Stationsnamen nicht, wird BadRequest geliefert.
        ///     Wurde der Eintrag erstellt, liefert der Controller NoContent.
        /// </summary>
        [HttpPost("{id}/tests")]
        public IActionResult AddTest(int id, TestterminDto testterminDto)
        {
            var station = _db.TestStations.SingleOrDefault(t => t.StationName == testterminDto.Station);
            if (station == null) { return BadRequest(); }
            var test = new Test
            {
                PersonId = id,
                Station = station,
                TestTimeStamp = testterminDto.TimeStamp
            };
            _db.Tests.Add(test);
            _db.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Erfasst das Ergebnis eines Tests.
        /// Hinweise:
        ///     Die ID ist die interne Test-ID.
        ///     Erstellen Sie eine geeignete DTO Klasse für die Übergabe des Payloads.
        ///     Sie können direkt Enum Typen definieren, ASP serialisiert den String korrekt.
        ///     Gibt es die Test-ID nicht, wird NotFound geliefert.
        ///     Wurde der Eintrag aktualisiert, wird NoContent geliefert.
        /// </summary>
        [HttpPut("/api/tests/{id}")]
        public IActionResult UpdateTest(int id, TestDto testDto)
        {
            var test = _db.Tests.SingleOrDefault(t => t.Id == id);
            if (test == null) { return NotFound(); }

            test.TestBay = testDto.TestBay;
            test.TestKitType = testDto.TestKitType;
            test.Result = testDto.Result;
            _db.SaveChanges();
            return NoContent();
        }
    }
}
