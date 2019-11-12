using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Weatherservice
{

    public class WeatherClient
    {
        private readonly string _url = "https://api.met.no/weatherapi/locationforecast/1.9";
        private readonly HttpClient _client = new HttpClient();
        /// <summary>
        /// Fordert ortsbasierende Wettervorhersagen an.
        /// </summary>
        /// <param name="location">Standort, für den die Vorhersage angefordert wird.</param>
        /// <returns></returns>
        public Task<ForecastSeries> GetLocationForecastAsync(Location location)
        {
            string url = $"{_url}/?lat={location.Latitude}&lon={location.Longitude}&msl={location.Height}";
            return RequestData(url, new ForecastSeries() { Location = location, RequestTime = DateTime.UtcNow });
        }

        /// <summary>
        /// Fordert die Daten von der MET Weather API für einen Standort an.
        /// </summary>
        /// <param name="url">URL, die aufgerufen wird.</param>
        /// <returns>ForecastSeries mit den geparsten Daten.</returns>
        private async Task<ForecastSeries> RequestData(string url, ForecastSeries series)
        {
            if (series == null) { throw new WeatherserviceException("Series is null."); }
            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new WeatherserviceException("Request not successful.") { Url = url, HttpStatusCode = (int)response.StatusCode };
            }
            using (Stream data = await response.Content.ReadAsStreamAsync())
            {
                using (XmlReader reader = XmlReader.Create(data))
                {
                    ForecastData forecastData = new ForecastData();
                    while (reader.Read())
                    {
                        if (reader.NodeType != XmlNodeType.Element) { continue; }
                        if (reader.Name == "model")
                        {
                            series.ModelName = reader.GetAttribute("name");
                            series.RunTime = reader.GetDateTime("termin", DateTime.MinValue);
                        }
                        if (reader.Name == "time")
                        {
                            DateTime forecastTo = reader.GetDateTime("to", null) ?? throw new Exception("Invalid Time Element");
                            if (series.ForecastsDictionary.ContainsKey(forecastTo))
                            {
                                forecastData = series.ForecastsDictionary[forecastTo];
                            }
                            else
                            {
                                forecastData = new ForecastData
                                {
                                    Time = forecastTo,
                                };
                                series.ForecastsDictionary.Add(forecastTo, forecastData);
                            }
                        }
                        try
                        {
                            if (reader.Name == "temperature") { forecastData.Temperature = reader.GetDecimal("value", null); }
                            if (reader.Name == "windDirection") { forecastData.WindDirection = reader.GetDecimal("deg", null); }
                            if (reader.Name == "windSpeed") { forecastData.WindSpeed = reader.GetDecimal("mps", null); }
                            if (reader.Name == "humidity") { forecastData.Humidity = reader.GetDecimal("value", null); }
                            if (reader.Name == "pressure") { forecastData.Pressure = reader.GetDecimal("value", null); }
                            if (reader.Name == "cloudiness") { forecastData.Cloudiness = reader.GetDecimal("percent", null); }
                            if (reader.Name == "fog") { forecastData.Fog = reader.GetDecimal("percent", null); }
                            if (reader.Name == "lowClouds") { forecastData.LowClouds = reader.GetDecimal("percent", null); }
                            if (reader.Name == "mediumClouds") { forecastData.MediumClouds = reader.GetDecimal("percent", null); }
                            if (reader.Name == "highClouds") { forecastData.HighClouds = reader.GetDecimal("percent", null); }
                            if (reader.Name == "dewpointTemperature") { forecastData.DewpointTemperature = reader.GetDecimal("value", null); }
                            if (reader.Name == "precipitation") { forecastData.Precipitation6h = reader.GetDecimal("value", null); }
                            if (reader.Name == "minTemperature") { forecastData.MinTemperature6h = reader.GetDecimal("value", null); }
                            if (reader.Name == "maxTemperature") { forecastData.MaxTemperature6h = reader.GetDecimal("value", null); }
                            if (reader.Name == "symbol") { forecastData.SignificantWeather = (SignificantWeather?)reader.GetInt("number", null); }
                        }
                        catch { }
                    }
                }
            }
            return series;
        }
    }
}
