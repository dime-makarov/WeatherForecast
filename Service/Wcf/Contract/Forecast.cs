using System;
using System.Runtime.Serialization;

namespace Dm.WeatherForecast.Service.Wcf.Contract
{
    [DataContract]
    public class Forecast
    {
        [DataMember]
        public int CityId { get; set; }

        [DataMember]
        public DateTime TargetDate { get; set; }

        [DataMember]
        public int Temperature { get; set; }

        [DataMember]
        public int WindSpeed { get; set; }

        [DataMember]
        public string WindDirection { get; set; }

        [DataMember]
        public int Pressure { get; set; }

        [DataMember]
        public int Humidity { get; set; }
    }
}
