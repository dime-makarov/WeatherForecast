using System;
using System.ServiceModel;
using Dm.WeatherForecast.Service.Wcf.Forecast;

namespace Dm.WeatherForecast.Service.Wcf.Host
{
    public class Program : IDisposable
    {
        static void Main(string[] args)
        {
            using (var app = new Program())
            {
                app.Run();
            }
        }

        /// <summary>
        /// Run
        /// </summary>
        public void Run()
        {
            using (var host = new ServiceHost(typeof(ForecastService)))
            {
                host.Open();

                Console.WriteLine("Service hosted sucessfully.");
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();

                host.Close();
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // Dispose logic
        }
    }
}
