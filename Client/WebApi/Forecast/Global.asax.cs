using System.Net.Http.Formatting;
using System.Web.Http;

namespace Dm.WeatherForecast.Client.WebApi.Forecast
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
