using System.Collections.Generic;

namespace Dm.WeatherForecast.Client.Grabber.Contract
{
    public interface IGrabber
    {
        IEnumerable<City> GrabCities();

        IEnumerable<Forecast> GrabForecastForTomorrow(City city);
    }
}
