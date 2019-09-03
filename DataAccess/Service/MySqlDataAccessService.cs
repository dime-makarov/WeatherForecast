using System;
using System.Collections.Generic;
using System.Linq;
using Dm.WeatherForecast.DataAccess.Contract;
using Dm.WeatherForecast.DataAccess.Service.Entities;
using Nelibur.ObjectMapper;
using NPoco;

namespace Dm.WeatherForecast.DataAccess.Service
{
    public class MySqlDataAccessService : IForecastDataAccess
    {
        public MySqlDataAccessService()
        {
            // TODO: Connection string to config file
            DatabaseInstance = new Database(
                @"Server=sql7.freemysqlhosting.net;Database=sql7302023;Uid=sql7302023;Pwd=C8VVCtPIIP;Charset=utf8;",
                DatabaseType.MySQL,
                MySql.Data.MySqlClient.MySqlClientFactory.Instance);

            // Mapping configuration
            TinyMapper.Bind<CityEntity, City>();
            TinyMapper.Bind<ForecastEntity, Forecast>();
        }

        /// <summary>
        /// Database instance
        /// </summary>
        protected IDatabase DatabaseInstance;

        /// <summary>
        /// Get all cities
        /// </summary>
        public IEnumerable<City> GetCities()
        {
            var cities = DatabaseInstance.Fetch<CityEntity>();

            return TinyMapper.Map<List<City>>(cities);
        }

        /// <summary>
        /// Add new city
        /// </summary>
        /// <returns>Id of new inserted city</returns>
        public int AddCity(City newCity)
        {
            var cityEntity = TinyMapper.Map<CityEntity>(newCity);

            var result = DatabaseInstance.Insert<CityEntity>(cityEntity);

            // MySQL provider returns result of ulong type.
            // Perform double-cast to int (which is more general).
            return (int)(ulong)result;
        }

        /// <summary>
        /// Get city by name
        /// </summary>
        /// <returns>City entity or null</returns>
        public City GetCityByName(string cityName)
        {
            var city = DatabaseInstance.Query<CityEntity>().Where(c => c.Name == cityName).FirstOrDefault();

            if (city == null)
            {
                return null;
            }

            return TinyMapper.Map<City>(city);
        }

        /// <summary>
        /// Get forecast for particular city
        /// </summary>
        public IEnumerable<Forecast> GetForecast(int cityId, DateTime targetDate)
        {
            var forecasts = DatabaseInstance
                .Query<ForecastEntity>()
                .Where(f => f.CityId == cityId)
                .ToList()
                // Really bad practice to use Where() after materialization.
                // But can't include condition for dates in Where-clause above.
                // Seems as NPoco's issue.
                // TODO: Investigate.
                .Where(f => f.TargetDate.Date == targetDate.Date)
                .ToList();
            
            return TinyMapper.Map<List<Forecast>>(forecasts);
        }

        /// <summary>
        /// Add new or update existing forecast
        /// </summary>
        /// <returns></returns>
        public void AddOrUpdateForecast(Forecast newForecast)
        {
            var forecastEntity = TinyMapper.Map<ForecastEntity>(newForecast);

            DatabaseInstance.Save<ForecastEntity>(forecastEntity);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (DatabaseInstance != null)
            {
                DatabaseInstance.Dispose();
            }
        }
    }
}
