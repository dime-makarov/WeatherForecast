using Dm.WeatherForecast.Service.Wcf.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Dm.WeatherForecast.Client.ForecastService
{
    public class ForecastServiceClient : IDisposable
    {
        protected ChannelFactory<IForecastService> ChannelFactory;

        protected IForecastService ServiceChannel;

        /// <summary>
        /// Ctor
        /// </summary>
        public ForecastServiceClient(string serviceHostName)
        {
            var bnd = new WSHttpBinding();
            var endpAddr = new EndpointAddress(string.Format("http://{0}/ForecastService.svc", serviceHostName));

            ChannelFactory = new ChannelFactory<IForecastService>(bnd, endpAddr);
            ServiceChannel = ChannelFactory.CreateChannel();
        }

        /// <summary>
        /// Get cities
        /// </summary>
        /// <returns></returns>
        public List<City> GetCities()
        {
            var cities = ServiceChannel.GetCities();

            if (!cities.Any())
            {
                return new List<City>(0);
            }

            return cities;
        }

        /// <summary>
        /// Load forecasts
        /// </summary>
        public List<Forecast> GetForecast(int cityId, DateTime targetDate)
        {
            var forecasts = ServiceChannel.GetForecast(cityId, targetDate);

            if (!forecasts.Any())
            {
                return new List<Forecast>(0);
            }

            return forecasts;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)ChannelFactory).Dispose();
        }

    }
}
