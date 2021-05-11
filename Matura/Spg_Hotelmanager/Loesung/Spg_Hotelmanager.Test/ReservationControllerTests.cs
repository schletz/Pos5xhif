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
                var result = controller.GetReservations(RoomCategory.Premium, default) as OkObjectResult;
                Assert.NotNull(result);  // Wurde HTTP 200 geliefert?

                var reservations = result.Value as IEnumerable<Reservation>;
                Assert.True(reservations.Count() == 6);
                //Assert.All(reservations, r => r.Room.RoomCategory == RoomCategory.Premium);
            }
        }

        [Fact]
        public void GetReservationsReturnsNotFoundWhenCategoryIsInvalidTest()
        {
            using (var db = GetContext())
            {
                var controller = new ReservationController(db);
                var result = controller.GetReservations((RoomCategory) 10, default) as NotFoundResult;
                Assert.NotNull(result);  // Wurde HTTP 200 geliefert?
            }
        }

        [Fact]
        public void AddReservationSuccessTest()
        {
            using (var db = GetContext())
            {
                var controller = new ReservationController(db);
                var request = new ReservationController.ReservationRequest
                {
                    CustomerId = db.Customers.FirstOrDefault().Id,
                    EmployeeId = db.Employees.FirstOrDefault().Id,
                    Nights = 4,
                    ReservationFrom = new DateTime(2022, 1, 1)
                };
                var result = controller.AddReservation(request) as OkObjectResult;
                Assert.NotNull(result);  // Wurde HTTP 200 geliefert?

                var reservation = result.Value as Reservation;
                Assert.True(reservation.Id != default);
            }
        }

        [Fact]
        public void AddReservationReturnsBadRequestWhenOverlappingTest()
        {
            using (var db = GetContext())
            {
                var controller = new ReservationController(db);
                var customerId = db.Customers.FirstOrDefault().Id;
                var request = new ReservationController.ReservationRequest
                {
                    CustomerId = customerId,
                    EmployeeId = db.Employees.FirstOrDefault().Id,
                    Nights = 1,
                    ReservationFrom = db.Reservations.FirstOrDefault(r => r.CustomerId == customerId).ReservationFrom
                };
                var result = controller.AddReservation(request) as BadRequestResult;
                Assert.NotNull(result);  // Wurde HTTP 200 geliefert?
            }
        }

    }
}
