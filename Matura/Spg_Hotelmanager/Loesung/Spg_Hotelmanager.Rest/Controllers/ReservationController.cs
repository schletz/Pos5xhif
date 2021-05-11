using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spg_Hotelmanager.Application.Domain;
using Spg_Hotelmanager.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg_Hotelmanager.Rest.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private readonly HotelContext _db;

        public ReservationController(HotelContext db)
        {
            _db = db;
        }

        /*
         * {
            ReservationFrom: '2021-04-06',
            Nights: 4,
            CustomerId: 10,
            EmployeeId: 1
        }
         */
        public class ReservationRequest
        {
            public DateTime ReservationFrom { get; set; }
            public int Nights { get; set; }
            public int CustomerId { get; set; }
            public int EmployeeId { get; set; }
        }

        /// <summary>
        /// GET /api/reservation/{Category}
        /// z. B. https://localhost:44325/api/reservation/1
        /// z. B. https://localhost:44325/api/reservation/1?dateFrom=2020-10-25
        /// </summary>
        [HttpGet("{category}")]
        public IActionResult GetReservations(RoomCategory category, [FromQuery] DateTime? dateFrom)
        {
            if (category > RoomCategory.Suite)
            {
                return NotFound();
            }
            List<Reservation> result = _db.Reservations
                .Include(r => r.Room)
                .Where(r => r.Room.RoomCategory == category && r.ReservationFrom >= (dateFrom ?? DateTime.MinValue))
                .ToList();
            return Ok(result);   // Liefert HTTP 200 mit diesen Daten.
        }

        [HttpPost]
        public IActionResult AddReservation(ReservationRequest reservationRequest)
        {
            if (!_db.Reservations.Where(r => r.CustomerId == reservationRequest.CustomerId)
                .All(r => r.ReservationTo < reservationRequest.ReservationFrom
                || r.ReservationFrom > reservationRequest.ReservationFrom.AddDays(reservationRequest.Nights)))
            {
                return BadRequest();
            }
            var reservation = new Reservation
            {
                CustomerId = reservationRequest.CustomerId,
                EmployeeId = reservationRequest.EmployeeId,
                ReservationFrom = reservationRequest.ReservationFrom,
                ReservationTo = reservationRequest.ReservationFrom.AddDays(reservationRequest.Nights)
            };
            _db.Reservations.Add(reservation);
            _db.SaveChanges();

            return Ok(reservation);
        }
    }
}
