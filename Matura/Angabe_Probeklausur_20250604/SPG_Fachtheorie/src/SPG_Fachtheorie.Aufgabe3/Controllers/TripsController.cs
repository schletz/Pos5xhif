using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3.Commands;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System.Linq;
using System.Text.RegularExpressions;

namespace SPG_Fachtheorie.Aufgabe3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ScooterContext _db;

        public TripsController(ScooterContext db)
        {
            _db = db;
        }

        /// <summary>
        /// TODO: Füge hier die Parameter der Controllermethode ein und implementiere den Endpunkt.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType<List<TripDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TripDto>>> GetTripById()
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        /// <summary>
        /// TODO: Füge hier die Parameter der Controllermethode ein und implementiere den Endpunkt.
        /// </summary>
        public async Task<IActionResult> PatchTripsByKey()
        {
            throw new NotImplementedException();
        }

    }
}
