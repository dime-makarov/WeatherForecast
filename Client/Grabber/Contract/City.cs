
namespace Dm.WeatherForecast.Client.Grabber.Contract
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}] {1} ({2})", Id, Name, Url);
        }
    }
}
