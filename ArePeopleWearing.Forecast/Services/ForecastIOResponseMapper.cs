using ForecastIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts.Services
{
    public class ForecastIOResponseMapper
    {
        public Forecast MapResponse(ForecastIOResponse response)
        {
            var forecast = new Forecast();
            forecast.Latitude = response.latitude;
            forecast.Longitude = response.longitude;
            forecast.MinimumTemperature = response.daily.data.ElementAt(0).temperatureMin;
            forecast.MaximumTemperature = response.daily.data.ElementAt(0).temperatureMax;
            forecast.PrecipitationProbability = response.daily.data.ElementAt(0).precipProbability;
            forecast.CloudCover = response.daily.data.ElementAt(0).cloudCover;
            forecast.Date = DateTime.Now;

            return forecast;
        }
    }
}
