using System;
using System.Collections.Generic;
using Dm.WeatherForecast.DataAccess.Contract;
using Dm.WeatherForecast.DataAccess.Service.Sqlite;
using Dm.WeatherForecast.Service.Wcf.Contract;
using Nelibur.ObjectMapper;

namespace Dm.WeatherForecast.Service.Wcf.Forecast
{
    public class ForecastService : IForecastService, IDisposable
    {
        public ForecastService()
        {
            DataAccess = new SqliteDataAccessService();

            // Mapping configuration (from DataAccess to WCF contracts)
            TinyMapper.Bind<DataAccess.Contract.City, Contract.City>();
            TinyMapper.Bind<DataAccess.Contract.Forecast, Contract.Forecast>();
        }

        protected IForecastDataAccess DataAccess;

        /// <summary>
        /// Get cities
        /// </summary>
        public List<Contract.City> GetCities()
        {
            var cities = DataAccess.GetCities();

            return TinyMapper.Map<List<Contract.City>>(cities);
        }

        /// <summary>
        /// Get forecast
        /// </summary>
        public List<Contract.Forecast> GetForecast(int cityId, DateTime targetDate)
        {
            var forecasts = DataAccess.GetForecast(cityId, targetDate);

            return TinyMapper.Map<List<Contract.Forecast>>(forecasts);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (DataAccess != null)
            {
                DataAccess.Dispose();
            }
        }
    }
}
