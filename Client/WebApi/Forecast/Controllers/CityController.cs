using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Http;
using Dm.WeatherForecast.Client.ForecastService;
using Dm.WeatherForecast.Service.Wcf.Contract;

namespace Dm.WeatherForecast.Client.WebApi.Forecast.Controllers
{
    public class CityController : ApiController
    {
        [HttpGet]
        public List<City> Get()
        {
            List<City> cities;
            string hostName = WebConfigurationManager.AppSettings["ForecastServiceHostName"];

            using (ForecastServiceClient client = new ForecastServiceClient(hostName))
            {
                cities = client.GetCities();
            }

            return cities;
        }
    }
}
