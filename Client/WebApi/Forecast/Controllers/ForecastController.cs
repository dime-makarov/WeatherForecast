using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace Dm.WeatherForecast.Client.WebApi.Forecast.Controllers
{
    public class ForecastController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Forecast";
        }
    }
}
