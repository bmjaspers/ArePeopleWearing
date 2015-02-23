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
            forecast.MinimumTemperature = (double)(response.daily.data.ElementAt(0).temperatureMin);
            forecast.MaximumTemperature = (double)(response.daily.data.ElementAt(0).temperatureMax);
            forecast.PrecipitationProbability = (double)(response.daily.data.ElementAt(0).precipProbability);
            forecast.CloudCover = (double)(response.daily.data.ElementAt(0).cloudCover);

            return forecast;
        }
    }
}
