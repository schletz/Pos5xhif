using System;

namespace Weatherservice
{
    public class ForecastData : IEquatable<ForecastData>
    {
        public DateTime Time { get; internal set; }
        public decimal? Temperature { get; internal set; }
        public decimal? WindDirection { get; internal set; }
        public decimal? WindSpeed { get; internal set; }
        public decimal? Humidity { get; internal set; }
        public decimal? Pressure { get; internal set; }
        public decimal? Cloudiness { get; internal set; }
        public decimal? Fog { get; internal set; }
        public decimal? LowClouds { get; internal set; }
        public decimal? MediumClouds { get; internal set; }
        public decimal? HighClouds { get; internal set; }
        public decimal? DewpointTemperature { get; internal set; }
        public decimal? Precipitation6h { get; internal set; }
        public decimal? MinTemperature6h { get; internal set; }
        public decimal? MaxTemperature6h { get; internal set; }
        public SignificantWeather? SignificantWeather { get; internal set; }
        public override bool Equals(object obj) => Equals(obj as ForecastData);
        public bool Equals(ForecastData other) => Time.Equals(other?.Time);
        public override int GetHashCode() => Time.GetHashCode();
    }
}
