using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Dm.WeatherForecast.Client.Grabber.Contract;

namespace Dm.WeatherForecast.Client.Grabber.Gismeteo
{
    public class GismeteoGrabber : IGrabber
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public GismeteoGrabber()
        {
            Web = new HtmlWeb();
            IndexToHour = new Dictionary<int, int>
            {
                { 0, 1 },
                { 1, 4 },
                { 2, 7 },
                { 3, 10 },
                { 4, 13 },
                { 5, 16 },
                { 6, 19 },
                { 7, 22 }
            };
        }

        /// <summary>
        /// HtmlAgilityPack web client
        /// </summary>
        protected HtmlWeb Web;

        protected Dictionary<int, int> IndexToHour;

        protected int MaxDatasetCount = 8; // Why 8?
                                           // Because we can't have more than 8 datasets for a day
                                           // (according to Gismeteo website structure)


        /// <summary>
        /// Grab cities
        /// </summary>
        public virtual IEnumerable<City> GrabCities()
        {
            var result = new List<City>();

            HtmlDocument doc = Web.Load("https://www.gismeteo.ru/");
            var nodes = doc.DocumentNode.SelectNodes("//a[contains(@class, 'cities_link')]");

            foreach (var node in nodes)
            {
                string cityName = node.SelectSingleNode("span[@class='cities_name']").InnerText.Trim();
                string cityUrl = "https://www.gismeteo.ru" + node.Attributes["href"].Value + "tomorrow";

                result.Add(new City
                {
                    Id = 0,
                    Name = cityName,
                    Url = cityUrl
                });
            }

            return result;
        }

        /// <summary>
        /// Grab forecast for tomorrow
        /// </summary>
        /// <param name="city">Target city</param>
        public virtual IEnumerable<Forecast> GrabForecastForTomorrow(City city)
        {
            List<Forecast> forecasts = new List<Forecast>(MaxDatasetCount); 

            HtmlDocument doc = Web.Load(city.Url);

            List<int> temps = GetTemperature(doc); ;
            List<Wind> winds = GetWind(doc); ;
            List<int> pressures = GetPressure(doc); ;
            List<int> humidities = GetHumidity(doc); ;

            // Insert into database
            for (int i = 0; i < MaxDatasetCount; i++)
            {
                var now = DateTime.Now;
                var targetDate = new DateTime(now.Year, now.Month, now.Day, IndexToHour[i], 0, 0);
                // Use AddDays to handle switch to the next month
                targetDate = targetDate.AddDays(1);

                Forecast forecast = new Forecast
                {
                    CityId = city.Id,
                    TargetDate = targetDate,
                    Temperature = temps[i],
                    Wind = winds[i],
                    Pressure = pressures[i],
                    Humidity = humidities[i]
                };

                forecasts.Add(forecast);
            }

            return forecasts;
        }

        /// <summary>
        /// Get temperature
        /// </summary>
        /// <param name="doc">Source document</param>
        protected virtual List<int> GetTemperature(HtmlDocument doc)
        {
            var result = new List<int>();
            int expectedCount = MaxDatasetCount;

            var tempChartNode = doc.DocumentNode.SelectNodes("//div[contains(@class, 'chart__temperature')]")[3];
            var tempNodes = tempChartNode.SelectNodes("div[@class='values']/div[@class='value']/span[contains(@class, 'unit_temperature_c')]");

            foreach (var tempNode in tempNodes)
            {
                string tempStr = tempNode.InnerText.Trim();

                result.Add(int.Parse(tempStr));
            }

            if (result.Count != expectedCount)
            {
                throw new Exception(string.Format("Wrong count of records in temperature data. Expected count: {0}. Actual count: {0}", expectedCount, result.Count));
            }

            return result;
        }

        /// <summary>
        /// Get wind
        /// </summary>
        /// <param name="doc">Source document</param>
        protected virtual List<Wind> GetWind(HtmlDocument doc)
        {
            var result = new List<Wind>();
            int expectedCount = MaxDatasetCount;

            var windNodes = doc.DocumentNode.SelectNodes("//div[@class='widget__row widget__row_table widget__row_wind']/div[@class='widget__item']/div[@class='w_wind']");

            foreach (var windNode in windNodes)
            {
                string windSpeedStr = windNode.SelectSingleNode("div/span[@class='unit unit_wind_m_s']").InnerText.Trim();
                string windDirection = windNode.SelectSingleNode("div[@class='w_wind__direction gray']").InnerText.Trim();

                result.Add(new Wind
                {
                    Speed = int.Parse(windSpeedStr),
                    Direction = windDirection
                });
            }

            if (result.Count != expectedCount)
            {
                throw new Exception(string.Format("Wrong count of records in wind data. Expected count: {0}. Actual count: {0}", expectedCount, result.Count));
            }

            return result;
        }

        /// <summary>
        /// Get pressure
        /// </summary>
        /// <param name="doc">Source document</param>
        protected virtual List<int> GetPressure(HtmlDocument doc)
        {
            var result = new List<int>();
            int expectedCount = MaxDatasetCount;

            var nodes = doc.DocumentNode.SelectNodes("//div[@data-widget-id='pressure']/div[@class='widget__body']//span[@class='unit unit_pressure_mm_hg_atm']");

            foreach (var node in nodes)
            {
                string pressureStr = node.InnerText.Trim();

                result.Add(int.Parse(pressureStr));
            }

            if (result.Count != expectedCount)
            {
                throw new Exception(string.Format("Wrong count of records in pressure data. Expected count: {0}. Actual count: {0}", expectedCount, result.Count));
            }

            return result;
        }

        /// <summary>
        /// Get humidity
        /// </summary>
        /// <param name="doc">Source document</param>
        protected virtual List<int> GetHumidity(HtmlDocument doc)
        {
            var result = new List<int>();
            int expectedCount = MaxDatasetCount;

            var nodes = doc.DocumentNode.SelectNodes("//div[@data-widget-id='humidity']/div[@class='widget__body']//div[contains(@class, 'w-humidity widget__value')]");

            foreach (var node in nodes)
            {
                string humStr = node.InnerText.Trim();

                result.Add(int.Parse(humStr));
            }

            if (result.Count != expectedCount)
            {
                throw new Exception(string.Format("Wrong count of records in humidity data. Expected count: {0}. Actual count: {0}", expectedCount, result.Count));
            }

            return result;
        }
    }
}
