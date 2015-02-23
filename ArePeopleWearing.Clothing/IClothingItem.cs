using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArePeopleWearing.Forecasts;

namespace ArePeopleWearing.Clothing
{
    public interface IClothingItem
    {
        WearingClothingResult IsBeingWorn(Forecast forecast);
    }
}
