using System;

namespace Weatherservice
{
    public struct Location : IEquatable<Location>
    {
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public decimal Height { get; }
        public Location(decimal latitude, decimal longitude) : this(latitude, longitude, 0) { }
        public Location(decimal latitude, decimal longitude, decimal height)
        {
            Longitude = longitude;
            Latitude = latitude;
            Height = height;
        }

        public bool Equals(Location other) => Latitude == other.Latitude && Longitude == other.Longitude && Height == other.Height;
    }
}
