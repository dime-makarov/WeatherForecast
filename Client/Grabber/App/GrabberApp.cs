using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using Dm.WeatherForecast.Client.Grabber.Contract;
using Dm.WeatherForecast.Client.Grabber.Gismeteo;
using Dm.WeatherForecast.DataAccess.Contract;
using Dm.WeatherForecast.DataAccess.Service;
using Nelibur.ObjectMapper;

namespace Dm.WeatherForecast.Client.Grabber.App
{
    public class GrabberApp : IDisposable
    {
        /// <summary>
        /// Entry point
        /// </summary>
        static void Main()
        {
            using (var app = new GrabberApp())
            {
                app.Run();
            }
        }

        protected IForecastDataAccess DataAccess;

        protected IGrabber Grabber;

        protected Timer Timer;

        // Flag to prevent executing grab process if previous one isn't finished
        protected bool GrabIsInProcess;

        /// <summary>
        /// Ctor
        /// </summary>
        public GrabberApp()
        {
            DataAccess = new MySqlDataAccessService();
            Grabber = new GismeteoGrabber();
            TypeDescriptor.AddAttributes(typeof(Dm.WeatherForecast.Client.Grabber.Contract.Forecast), new TypeConverterAttribute(typeof(GrabberForecastConverter)));
            TinyMapper.Bind<Dm.WeatherForecast.Client.Grabber.Contract.Forecast, Dm.WeatherForecast.DataAccess.Contract.Forecast>();
        }

        /// <summary>
        /// Run
        /// </summary>
        public void Run()
        {
            // Forced first run
            Grab();

            Timer = new Timer(60000); // 1 min
            Timer.Elapsed += OnTimerEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;

            Console.ReadLine();
        }

        /// <summary>
        /// On timer event
        /// </summary>
        protected virtual void OnTimerEvent(object sender, ElapsedEventArgs e)
        {
            Grab();
        }

        /// <summary>
        /// Grab once
        /// </summary>
        protected virtual void Grab()
        {
            if (GrabIsInProcess)
            {
                return;
            }

            GrabIsInProcess = true;

            var cities = Grabber.GrabCities().ToList();

            cities = HandleUnknownCities(cities);

            // Process all the cities
            foreach (var city in cities)
            {
                Console.WriteLine("Fetching data for: {0}", city.ToString());

                var forecasts = Grabber.GrabForecastForTomorrow(city).ToList();

                foreach (var forecast in forecasts)
                {
                    var daForecast = TinyMapper.Map<Dm.WeatherForecast.DataAccess.Contract.Forecast>(forecast);
                    DataAccess.AddOrUpdateForecast(daForecast);
                }
            }

            GrabIsInProcess = false;
        }

        /// <summary>
        /// Handle unknown cities
        /// </summary>
        /// <remarks>Add unknown cities to database and update their Id in the list</remarks>
        protected virtual List<Dm.WeatherForecast.Client.Grabber.Contract.City> HandleUnknownCities(List<Dm.WeatherForecast.Client.Grabber.Contract.City> cities)
        {
            foreach (var city in cities)
            {
                var existingCity = DataAccess.GetCityByName(city.Name);

                if (existingCity == null)
                {
                    var newId = DataAccess.AddCity(new Dm.WeatherForecast.DataAccess.Contract.City { Name = city.Name });
                    city.Id = newId;
                }
                else
                {
                    city.Id = existingCity.Id;
                }
            }

            return cities;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (DataAccess != null)
            {
                DataAccess.Dispose();
            }

            if (Timer != null)
            {
                Timer.Dispose();
            }
        }
    }
}
