using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts.Services
{
    public interface IForecastService
    {
        Task<Forecast> GetForecast(float latitude, float longitude);
    }
}
