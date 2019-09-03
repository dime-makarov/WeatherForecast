using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Dm.WeatherForecast.Service.Wcf.Contract
{
    [ServiceContract]
    public interface IForecastService
    {
        [OperationContract]
        List<City> GetCities();

        [OperationContract]
        List<Forecast> GetForecast(int cityId, DateTime targetDate);
    }
}
