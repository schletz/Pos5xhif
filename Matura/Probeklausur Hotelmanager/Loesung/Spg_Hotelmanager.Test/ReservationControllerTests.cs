using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spg_Hotelmanager.Application.Domain;
using Spg_Hotelmanager.Application.Infrastructure;
using Spg_Hotelmanager.Rest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Spg_Hotelmanager.Test
{
    public class ReservationControllerTests
    {
        private HotelContext GetContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Hotel.db")
                .Options;
            var db = new HotelContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Import("data.sql");
            return db;
        }

        [Fact]
        public void GetReservationsSuccessTest()
        {
            using (var db = GetContext())
            {
                var controller = new ReservationController(db);
                var result = controller.GetReservations(RoomCategory.Superior, default) as OkObjectResult;
                Assert.NotNull(result);   // Wurde HTTP 200 geliefert?

                //var reservations = result.Value as List<Reservation>;
                var reservations = result.Value as IEnumerable<Reservation>;
                Assert.NotNull(reservations);
                Assert.True(reservations.Count() == 28);
            }
        }

        [Fact]
        public void GetReservationsReturnsNotFoundIfCategoryInvalidTest()
        {
            using (var db = GetContext())
            {
                var controller = new ReservationController(db);
                var result = controller.GetReservations((RoomCategory)10, default) as NotFoundResult;
                Assert.NotNull(result);   // Wurde HTTP 404 geliefert?
            }
        }

        [Fact]
        public void AddReservationSuccessTest()
        {
            using (var db = GetContext())
            {
                var controller = new ReservationController(db);
                var reservationRequest = new ReservationController.ReservationRequest
                {
                    CustomerId = db.Customers.FirstOrDefault().Id,
                    EmployeeId = db.Employees.FirstOrDefault().Id,
                    ReservationFrom = db.Reservations.Max(r => r.ReservationTo).AddDays(1),
                    Nights = 4
                };
                var result = controller.AddReservation(reservationRequest) as OkObjectResult;
                Assert.NotNull(result);   // Wurde HTTP 200 geliefert?
                var reservation = result.Value as Reservation;
                Assert.NotNull(reservation);
                Assert.True(reservation.Id != default);
            }
        }

        [Fact]
        public void AddReservationReturnsBadRequestIfReservationIsOverlappingTest()
        {
            using (var db = GetContext())
            {
                var controller = new ReservationController(db);
                var reservation = db.Reservations.FirstOrDefault();
                var reservationRequest = new ReservationController.ReservationRequest
                {
                    CustomerId = reservation.CustomerId,
                    EmployeeId = reservation.EmployeeId,
                    ReservationFrom = reservation.ReservationFrom,
                    Nights = 4
                };
                var result = controller.AddReservation(reservationRequest) as BadRequestResult;
                Assert.NotNull(result);   // Wurde HTTP 200 geliefert?
            }
        }

    }
}
