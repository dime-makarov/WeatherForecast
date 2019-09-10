using System.Web.Http;

namespace Dm.WeatherForecast.Client.WebApi.Forecast
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}",
                defaults: new { controller = "City" }
            );
        }
    }
}
