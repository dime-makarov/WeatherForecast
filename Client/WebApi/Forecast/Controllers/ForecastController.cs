using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Http;
using Dm.WeatherForecast.Client.ForecastService;
using WcfContract = Dm.WeatherForecast.Service.Wcf.Contract;

namespace Dm.WeatherForecast.Client.WebApi.Forecast.Controllers
{
    public class ForecastController : ApiController
    {
        [HttpGet]
        public List<WcfContract.Forecast> Get([FromUri]int cityId, [FromUri]DateTime targetDate)
        {
            List<WcfContract.Forecast> forecasts;
            string hostName = WebConfigurationManager.AppSettings["ForecastServiceHostName"];

            using (ForecastServiceClient client = new ForecastServiceClient(hostName))
            {
                forecasts = client.GetForecast(cityId, targetDate);
            }

            return forecasts;
        }
    }
}
