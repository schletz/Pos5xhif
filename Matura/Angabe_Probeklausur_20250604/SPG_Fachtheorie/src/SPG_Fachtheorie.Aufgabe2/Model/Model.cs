using System;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public class User
    {
        public User(string email, int freeKilometers)
        {
            Email = email;
            FreeKilometers = freeKilometers;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public int FreeKilometers { get; set; }
        public List<Trip> Trips { get; } = new();
    }
    public class Scooter
    {
        public Scooter(string manufacturerId, string model, decimal pricePerKilometer)
        {
            ManufacturerId = manufacturerId;
            Model = model;
            PricePerKilometer = pricePerKilometer;
        }

        public int Id { get; set; }
        public string ManufacturerId { get; set; }
        public string Model { get; set; }
        public decimal PricePerKilometer { get; set; }
        public List<Trip> Trips { get; } = new();
    }

    public class Location
    {
        public Location(decimal longitude, decimal latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }

    public class TripLog
    {
        protected TripLog() { }
        public TripLog(Trip trip, DateTime timestamp, Location location, int mileageInMeters)
        {
            Trip = trip;
            Timestamp = timestamp;
            Location = location;
            MileageInMeters = mileageInMeters;
        }

        public int Id { get; set; }
        public Trip Trip { get; set; }
        public DateTime Timestamp { get; set; }
        public Location Location { get; set; }
        public int MileageInMeters { get; set; }
    }
    public class Trip
    {
        protected Trip() { }
        public Trip(User user, Scooter scooter, DateTime begin, DateTime? end, Location? parkingLocation)
        {
            User = user;
            Scooter = scooter;
            Begin = begin;
            End = end;
            ParkingLocation = parkingLocation;
        }

        public int Id { get; set; }
        public User User { get; set; }
        public Scooter Scooter { get; set; }
        public DateTime Begin { get; set; }
        public DateTime? End { get; set; }
        public Location? ParkingLocation { get; set; }
        public List<TripLog> TripLogs { get; } = new();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}