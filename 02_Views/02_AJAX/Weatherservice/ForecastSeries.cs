using System;
using System.Collections.Generic;

namespace Weatherservice
{
    public class ForecastSeries
    {
        internal Dictionary<DateTime, ForecastData> ForecastsDictionary { get; } = new Dictionary<DateTime, ForecastData>();
        public Location Location { get; internal set; }
        public DateTime RunTime { get; internal set; }
        public IEnumerable<ForecastData> Forecasts => ForecastsDictionary.Values;

        public string ModelName { get; internal set; }
    }
}
