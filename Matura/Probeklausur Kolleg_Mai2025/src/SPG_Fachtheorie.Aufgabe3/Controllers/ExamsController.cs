using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3.Commands;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System.Net;
using System.Text.RegularExpressions;

namespace SPG_Fachtheorie.Aufgabe3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly ExamsContext _db;

        public ExamsController(ExamsContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Liefert Daten zu einer Prüfung mit der angegebenen id. Der Query Parameter includeAnswers bestimmt,
        /// ob die Antworten im Property „possibleAnswers“ ausgegeben werden. Ist der Parameter false, so wird
        /// ein leeres Array in possibleAnswers zurückgegeben.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType<ExamDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExamDto>> GetExamById(int id, [FromQuery] bool includeAnswers)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Dieser Endpunkt soll den Schwellenwert für „failed“ des Exams (failThreshold) verändern.
        /// </summary>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateThreshold(int id, [FromBody] UpdateThresholdCommand cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dieser Endpunkt soll ein Exam löschen. Es wird aber nicht mit einer DELETE Anweisung aus der
        /// Datenbank gelöscht, sondern es soll lediglich das Property Visible in Exam gesetzt werden (soft delete).
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExam(int id)
        {
            throw new NotImplementedException();
        }
    }
}
