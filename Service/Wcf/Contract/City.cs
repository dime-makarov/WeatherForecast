using System.Runtime.Serialization;

namespace Dm.WeatherForecast.Service.Wcf.Contract
{
    [DataContract]
    public class City
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
