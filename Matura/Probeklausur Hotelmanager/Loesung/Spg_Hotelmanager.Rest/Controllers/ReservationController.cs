using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public class ReservationRequest
        {
            public DateTime ReservationFrom { get; set; }
            public int Nights { get; set; }
            public int CustomerId { get; set; }
            public int EmployeeId { get; set; }
            public DateTime ReservationTo => ReservationFrom.AddDays(Nights);
        }

        public class InvoiceRequest
        {
            public decimal Consumption { get; set; }
        }

        /// <summary>
        /// Reagiert z. B. auf /api/reservation/1
        ///                    /api/reservation/1?dateFrom=2021-05-12
        /// </summary>
        [HttpGet("{category}")]
        public IActionResult GetReservations(RoomCategory category, [FromQuery] DateTime? dateFrom)
        {
            if (category > RoomCategory.Suite)
            {
                return NotFound();
            }
            var result = _db.Reservations
                .Where(r => r.Room.RoomCategory == category && r.ReservationFrom >= (dateFrom ?? DateTime.MinValue))
                .ToList();
            return Ok(result);  // Liefert HTTP 200 mit den angegebenen Daten als JSON.
        }

        [HttpPost]
        public IActionResult AddReservation(ReservationRequest reservationRequest)
        {
            if (!_db.Reservations
                .Where(r => r.CustomerId == reservationRequest.CustomerId)
                .All(r => r.ReservationTo < reservationRequest.ReservationFrom
                       || r.ReservationFrom > reservationRequest.ReservationTo))
            {
                return BadRequest();
            }
            var reservation = new Reservation
            {
                CustomerId = reservationRequest.CustomerId,
                EmployeeId = reservationRequest.EmployeeId,
                ReservationFrom = reservationRequest.ReservationFrom,
                ReservationTo = reservationRequest.ReservationTo
            };
            _db.Reservations.Add(reservation);
            _db.SaveChanges();

            return Ok(reservation);
        }

        [HttpPut("{reservationId}/{roomId}")]
        public IActionResult SetRoom(int reservationId, int roomId)
        {
            var found = _db.Reservations.Find(reservationId);
            if (found == null)
            {
                return NotFound();
            }
            found.RoomId = roomId;
            _db.SaveChanges();
            return Ok(found);
        }

        [HttpPut("invoice/{reservationId}")]
        public IActionResult SetInvoice(int reservationId, InvoiceRequest invoiceRequest)
        {
            var reservation = _db.Reservations.Find(reservationId);
            if (reservation == null)
            {
                return NotFound();
            }
            if (reservation.Room == null)
            {
                return BadRequest();
            }

            var nights = (int) (reservation.ReservationTo - reservation.ReservationFrom).TotalDays;
            reservation.InvoiceAmount = nights * reservation.Room.PricePerNight + invoiceRequest.Consumption;
            _db.SaveChanges();
            return Ok(reservation);
        }
    }
}
