using System;
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
            IForecastDataAccess svc = new SqliteDataAccessService();

            var cities = svc.GetCities();

            Assert.IsNotNull(cities);
            Assert.IsTrue(cities.Any());
        }

        [TestMethod]
        public void GetForecast_Test()
        {
            IForecastDataAccess svc = new SqliteDataAccessService();

            var forecasts = svc.GetForecast(1, DateTime.Now.AddDays(1));

            Assert.IsNotNull(forecasts);
            Assert.IsTrue(forecasts.Any());
        }

        [TestMethod]
        public void AddCity_Test()
        {
            IForecastDataAccess svc = new SqliteDataAccessService();

            int result = svc.AddCity(new City { Name = @"Киров" });

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetCityByName_Test()
        {
            IForecastDataAccess svc = new SqliteDataAccessService();

            City city = svc.GetCityByName(@"Киров");
        }

        [TestMethod]
        public void AddOrUpdateForecast_Test()
        {
            IForecastDataAccess svc = new SqliteDataAccessService();

            svc.AddOrUpdateForecast(new Forecast { CityId = 1, TargetDate = DateTime.Now });
        }
    }
}
