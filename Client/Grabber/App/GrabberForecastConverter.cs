using System;
using System.ComponentModel;
using System.Globalization;

namespace Dm.WeatherForecast.Client.Grabber.App
{
    public sealed class GrabberForecastConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(Dm.WeatherForecast.DataAccess.Contract.Forecast);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var src = (Dm.WeatherForecast.Client.Grabber.Contract.Forecast)value;
            var result = new Dm.WeatherForecast.DataAccess.Contract.Forecast
            {
                CityId = src.CityId,
                TargetDate = src.TargetDate,
                Temperature = src.Temperature,
                WindSpeed = src.Wind.Speed,
                WindDirection = src.Wind.Direction,
                Pressure = src.Pressure,
                Humidity = src.Humidity
            };

            return result;
        }
    }
}
