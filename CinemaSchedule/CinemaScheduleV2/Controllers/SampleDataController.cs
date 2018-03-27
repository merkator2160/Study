using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaScheduleV2.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static String[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public String DateFormatted { get; set; }
            public Int32 TemperatureC { get; set; }
            public String Summary { get; set; }

            public Int32 TemperatureF
            {
                get
                {
                    return 32 + (Int32)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
