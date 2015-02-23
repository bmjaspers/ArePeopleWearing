using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts
{
    // Domain model that encloses the values we care about when checking the forecast
    public class Forecast
    {
        [Key, Column(Order = 1)]
        public float Latitude { get; set; }

        [Key, Column(Order = 2)]
        public float Longitude { get; set; }

        [Key, Column(Order = 3)]
        public DateTime Date { get; set; }

        public float MinimumTemperature { get; set; }

        public float MaximumTemperature { get; set; }

        public float PrecipitationProbability { get; set; }

        public float CloudCover { get; set; }
    }
}
