using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts
{
    public class ForecastContext : DbContext
    {
        public ForecastContext() 
            : base("ForecastContext")
        {
        }

        public DbSet<Forecast> Forecasts { get; set; }
    }
}
