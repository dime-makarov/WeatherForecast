
namespace Dm.WeatherForecast.DataAccess.Service.Sqlite.Entities
{
    public class ForecastEntity
    {
        public int CityId { get; set; }

        public string TargetDate { get; set; }

        public int Temperature { get; set; }

        public int WindSpeed { get; set; }

        public string WindDirection { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }
    }
}
