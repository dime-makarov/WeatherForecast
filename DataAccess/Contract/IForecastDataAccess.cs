using System;
using System.Collections.Generic;

namespace Dm.WeatherForecast.DataAccess.Contract
{
    public interface IForecastDataAccess : IDisposable
    {
        IEnumerable<City> GetCities();

        int AddCity(City newCity);

        City GetCityByName(string cityName);

        IEnumerable<Forecast> GetForecast(int cityId, DateTime targetDate);

        void AddOrUpdateForecast(Forecast newForecast);
    }
}
