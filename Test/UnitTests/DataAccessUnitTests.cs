using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dm.WeatherForecast.DataAccess.Contract;
using Dm.WeatherForecast.DataAccess.Service.Sqlite;

namespace Dm.WeatherForecast.Test.UnitTests
{
    [TestClass]
    public class DataAccessUnitTests
    {
        [TestMethod]
        public void GetCities_Test()
        {
            IEnumerable<City> cities;

            using (IForecastDataAccess svc = new SqliteDataAccessService())
            {
                cities = svc.GetCities();
            }

            Assert.IsNotNull(cities);
            Assert.IsTrue(cities.Any());
        }

        [TestMethod]
        public void GetForecast_Test()
        {
            IEnumerable<Forecast> forecasts;

            using (IForecastDataAccess svc = new SqliteDataAccessService())
            {
                forecasts = svc.GetForecast(1, DateTime.Now);
            }

            Assert.IsNotNull(forecasts);
            Assert.IsTrue(forecasts.Any());
        }

        [TestMethod]
        public void AddCity_Test()
        {
            int result = 0;

            using (IForecastDataAccess svc = new SqliteDataAccessService())
            {
                result = svc.AddCity(new City { Name = @"Казань" });
            }

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetCityByName_Test()
        {
            using (IForecastDataAccess svc = new SqliteDataAccessService())
            {
                City city = svc.GetCityByName(@"Казань");
            }
        }

        [TestMethod]
        public void AddOrUpdateForecast_Test()
        {
            using (IForecastDataAccess svc = new SqliteDataAccessService())
            {
                DateTime now = DateTime.Now;
                DateTime targetDate = new DateTime(now.Year, now.Month, now.Day, 1, 0, 0);
                svc.AddOrUpdateForecast(new Forecast
                {
                    CityId = 1,
                    TargetDate = targetDate
                });
            }
        }
    }
}
