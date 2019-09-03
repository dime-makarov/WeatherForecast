
namespace Dm.WeatherForecast.Client.Viewer.ViewerApp.ViewModel
{
    public class CityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
