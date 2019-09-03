using System;

namespace Dm.WeatherForecast.DataAccess.Contract
{
    public class Forecast
    {
        public int CityId { get; set; }

        public DateTime TargetDate { get; set; }

        public int Temperature { get; set; }

        public int WindSpeed { get; set; }

        public string WindDirection { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }
    }
}
