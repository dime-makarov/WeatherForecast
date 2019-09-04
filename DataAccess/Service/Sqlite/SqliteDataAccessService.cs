using System;
using System.Collections.Generic;
using Dm.WeatherForecast.DataAccess.Contract;

namespace Dm.WeatherForecast.DataAccess.Service.Sqlite
{
    public class SqliteDataAccessService : IForecastDataAccess
    {
        /// <summary>
        /// Get all cities
        /// </summary>
        public IEnumerable<City> GetCities()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add new city
        /// </summary>
        /// <returns>Id of new inserted city</returns>
        public int AddCity(City newCity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get city by name
        /// </summary>
        /// <returns>City instance or null</returns>
        public City GetCityByName(string cityName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get forecast for particular city
        /// </summary>
        public IEnumerable<Forecast> GetForecast(int cityId, DateTime targetDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add new or update existing forecast
        /// </summary>
        /// <returns></returns>
        public void AddOrUpdateForecast(Forecast newForecast)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // Dispose logic
        }
    }
}
