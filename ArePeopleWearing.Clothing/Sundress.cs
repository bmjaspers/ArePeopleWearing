using ArePeopleWearing.Forecasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Clothing
{
    public class Sundress : IClothingItem
    {
        public WearingClothingResult IsBeingWorn(Forecast forecast)
        {
            if (forecast.PrecipitationProbability > 0.3)
            {
                return WearingClothingResult.No;
            }

            if (forecast.CloudCover > 0.4)
            {
                return WearingClothingResult.No;
            }

            if (forecast.MinimumTemperature > 75)
            {
                return WearingClothingResult.Yes;
            }

            if (forecast.MinimumTemperature >= 50 && forecast.MaximumTemperature > 75)
            {
                return WearingClothingResult.Maybe;
            }

            return WearingClothingResult.No;
        }
    }
}
