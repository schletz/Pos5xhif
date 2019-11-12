using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weatherservice;

namespace Webserver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForecastController : ControllerBase
    {
        private readonly WeatherClient _weatherClient;
        public ForecastController(WeatherClient weatherClient)
        {
            _weatherClient = weatherClient;
        }

        /// <summary>
        /// Reagiert auf GET /forecast?lat=48&lng=16.5&height=200
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ForecastSeries>> Get(
            [FromQuery(Name = "lat")]    decimal? latitude,
            [FromQuery(Name = "lng")]    decimal? longitude,
            [FromQuery(Name = "height")] decimal? height)
        {
            if (latitude == null || longitude == null) { return BadRequest(); }

            ForecastSeries forecast = await _weatherClient.GetLocationForecastAsync(
                new Location(latitude ?? 0, longitude ?? 0, height ?? 0));
            return Ok(forecast);
        }

        /// <summary>
        /// Reagiert auf GET /forecast/2019-11-15?lat=48&lng=16.5&height=200
        /// </summary>
        /// <param name="day"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [HttpGet("{day}")]
        public async Task<ActionResult<ForecastSeries>> GetDailyForecast(
            DateTime day,
            [FromQuery(Name = "lat")]    decimal? latitude,
            [FromQuery(Name = "lng")]    decimal? longitude,
            [FromQuery(Name = "height")] decimal? height)
        {
            if (latitude == null || longitude == null)
            {
                return BadRequest();
            }
            ForecastSeries forecast = await _weatherClient.GetLocationForecastAsync(
                new Location(latitude ?? 0, longitude ?? 0, height ?? 0));
            return Ok(new
            {
                forecast.Location,
                forecast.RunTime,
                Forecasts = forecast.Forecasts.Where(f => f.Time.Date == day.Date)
            });
        }
    }
}
