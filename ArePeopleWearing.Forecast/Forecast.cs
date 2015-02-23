using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts
{
    // Domain model that encloses the values we care about when checking the forecast
    public class Forecast
    {
        public double MinimumTemperature { get; set; }

        public double MaximumTemperature { get; set; }

        public double PrecipitationProbability { get; set; }

        public double CloudCover { get; set; }
    }
}
