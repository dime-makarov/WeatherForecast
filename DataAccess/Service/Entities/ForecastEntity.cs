using System;
using NPoco;

namespace Dm.WeatherForecast.DataAccess.Service.Entities
{
    [TableName("Forecasts")]
    [PrimaryKey("CityId,TargetDate")]
    public class ForecastEntity
    {
        [Column("CityId")]
        public int CityId { get; set; }

        [Column("TargetDate")]
        public DateTime TargetDate { get; set; }

        [Column("Temperature")]
        public int Temperature { get; set; }

        [Column("WindSpeed")]
        public int WindSpeed { get; set; }

        [Column("WindDirection")]
        public string WindDirection { get; set; }

        [Column("Pressure")]
        public int Pressure { get; set; }

        [Column("Humidity")]
        public int Humidity { get; set; }
    }
}
