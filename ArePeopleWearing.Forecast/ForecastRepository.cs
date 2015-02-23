using ArePeopleWearing.Forecasts.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts
{
    public class ForecastRepository
    {
        private ForecastContext _forecastContext;
        private IForecastService _forecastService;

        public ForecastRepository(ForecastContext forecastContext, IForecastService forecastService)
        {
            this._forecastContext = forecastContext;
            this._forecastService = forecastService;
        }

        public virtual async Task<Forecast> GetForecast(float latitude, float longitude)
        {
            var forecast = await _forecastContext.Forecasts.FirstOrDefaultAsync(f => f.Latitude == latitude
                && f.Longitude == longitude);

            if (forecast != null && forecast.Date.Date != DateTime.Today)
            {
                _forecastContext.Forecasts.Remove(forecast);        
                await _forecastContext.SaveChangesAsync();

                forecast = null;
            }

            if (forecast == null)
            {
                forecast = await _forecastService.GetForecast(latitude, longitude);
                _forecastContext.Forecasts.Add(forecast);
                await _forecastContext.SaveChangesAsync();
            }

             return forecast;
        }
    }
}
