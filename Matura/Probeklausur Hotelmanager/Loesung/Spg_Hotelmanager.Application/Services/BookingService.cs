using Spg_Hotelmanager.Application.Domain;
using Spg_Hotelmanager.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Spg_Hotelmanager.Application.Services
{
    public class BookingService
    {
        private readonly HotelContext _db;

        public BookingService(HotelContext db)
        {
            _db = db;
        }

        public class RevenueCategoryItem
        {
            public class CustomerDto
            {
                public int Id { get; set; }
                public string Firstname { get; set; }
                public string Lastname { get; set; }
                public string Street { get; set; }
                public string City { get; set; }
                public string Country { get; set; }
                public decimal Revenue { get; set; }
            }
            public int RevenueCategory { get; set; }
            public List<CustomerDto> Customers { get; set; }
        }

        public int? GetPreferredRoom(int customerId, RoomCategory category)
        {
            var reservations = _db.Reservations.Where(r =>
                r.CustomerId == customerId
                && r.Room.RoomCategory == category
                && r.Room.CanBeReserved);

            var lastReservation = reservations
                .OrderByDescending(r => r.ReservationFrom)
                .FirstOrDefault();
            return lastReservation?.RoomId;
        }

        public List<RevenueCategoryItem> GetCustomerRevenueCategories()
        {
            List<RevenueCategoryItem.CustomerDto> customers = _db.Customers
                .Select(c => new RevenueCategoryItem.CustomerDto
                {
                    Id = c.Id,
                    City = c.HomeAddress.City,
                    Country = c.HomeAddress.CountryCode,
                    Street = c.HomeAddress.Street,
                    Firstname = c.Name.Firstname,
                    Lastname = c.Name.Lastname,
                    Revenue = c.Reservations.Sum(r => r.InvoiceAmount) ?? 0
                })
                .ToList();

            List<RevenueCategoryItem> result = customers
                .GroupBy(c => 1 + (int)(c.Revenue / 10000))
                .Select(g => new RevenueCategoryItem
                {
                    RevenueCategory = g.Key,
                    Customers = g.ToList()
                })
                .ToList();
            return result;
        }
    }
}
