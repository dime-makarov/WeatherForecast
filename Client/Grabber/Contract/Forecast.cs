using System;

namespace Dm.WeatherForecast.Client.Grabber.Contract
{
    public class Forecast
    {
        public int CityId;

        public DateTime TargetDate;

        public int Temperature;

        public Wind Wind;

        public int Pressure;

        public int Humidity;
    }
}
