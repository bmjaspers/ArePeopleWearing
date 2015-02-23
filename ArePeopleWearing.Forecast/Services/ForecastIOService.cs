using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts.Services
{
    public class ForecastIOService : IForecastService
    {
        private ForecastIOClient _forecastIOClient;
        private ForecastIOResponseMapper _responseMapper;

        public ForecastIOService(ForecastIOClient forecastIOClient, ForecastIOResponseMapper responseMapper)
        {
            _forecastIOClient = forecastIOClient;
            this._responseMapper = responseMapper;
        }

        public async Task<Forecast> GetForecast(float latitude, float longitude)
        {
            return this._responseMapper.MapResponse(await _forecastIOClient.GetForecastResponse(latitude, longitude));
        }
    }
}
