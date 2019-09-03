using System;

namespace Dm.WeatherForecast.Client.Viewer.ViewerApp.ViewModel
{
    public class ForecastViewModel
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
