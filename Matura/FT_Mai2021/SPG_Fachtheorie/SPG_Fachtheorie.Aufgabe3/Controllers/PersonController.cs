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
    /// unabhängig von Ihrer Implementierung funktionstüchtig. Aufgabe1 verweist auf diese
    /// DLL, nicht auf Ihr Projekt.
    /// Verwenden Sie zum Testen die in PersonControllerTests vorgegebenen Testmethoden.
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

        [HttpGet]
        public IActionResult SayHello()
        {
            var count = _db.Tests.Count();
            return Ok($"Hello from PersonController. {count} Tests in der Datenbank.");
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
            throw new NotImplementedException();

        }

        /// <summary>
        /// Erzeugt einen Testtermin für eine existierende Person.
        /// Tauschen Sie object durch einen geeigneten Typ aus und fügen Sie die korrekte Routing Annotation ein.
        /// Hinweise:
        ///     Die ID ist die interne Personen-ID.
        ///     Erstellen Sie eine geeignete DTO Klasse für die Übergabe des Payloads.
        ///     Beim neu Anlegen eines Testtermines wird ein Test nur mit Person, Station und Timestamp angelegt.
        ///     Suchen Sie die Stations-ID durch den übergebenen Namen (er kann als eindeutig angenommen werden).
        ///     Gibt es die Person nicht, wird NotFound geliefert.
        ///     Gibt es die Stationsnamen nicht, wird BadRequest geliefert.
        ///     Wurde der Eintrag erstellt, liefert der Controller NoContent.
        /// </summary>
        public IActionResult AddTest(int id, object testterminDto)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Erfasst das Ergebnis eines Tests.
        /// Tauschen Sie object durch einen geeigneten Typ aus und fügen Sie die korrekte Routing Annotation ein.
        /// Hinweise:
        ///     Die ID ist die interne Test-ID.
        ///     Erstellen Sie eine geeignete DTO Klasse für die Übergabe des Payloads.
        ///     Sie können direkt Enum Typen definieren, ASP.NET serialisiert den String korrekt.
        ///     Gibt es die Test-ID nicht, wird NotFound geliefert.
        ///     Wurde der Eintrag aktualisiert, wird NoContent geliefert.
        /// </summary>
        public IActionResult UpdateTest(int id, object testDto)
        {
            throw new NotImplementedException();

        }
    }
}
