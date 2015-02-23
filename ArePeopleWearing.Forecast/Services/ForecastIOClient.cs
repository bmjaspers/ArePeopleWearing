using ForecastIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts.Services
{
    public class ForecastIOClient
    {
        private string _apiKey;

        public ForecastIOClient(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("apiKey");
            }

            this._apiKey = apiKey;
        }

        public virtual async Task<ForecastIOResponse> GetForecastResponse(float latitude, float longitude)
        {
            return await Task<ForecastIOResponse>.Run(() =>
            {
                var request = new ForecastIORequest(this._apiKey, latitude, longitude, DateTime.Now, Unit.us);
                return request.Get();
            });
        }
    }
}
