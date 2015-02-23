using ArePeopleWearing.Clothing;
using ArePeopleWearing.Forecasts;
using ArePeopleWearing.Forecasts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ArePeopleWearing.Controllers
{
    public class BeingWornController : ApiController
    {
        private ClothingItemFactory _clothingItemFactory;
        private IForecastService _forecastService;

        public BeingWornController(ClothingItemFactory clothingItemFactory, IForecastService forecastService)
        {
            _clothingItemFactory = clothingItemFactory;
            _forecastService = forecastService;
        }

        public async Task<IHttpActionResult> Get(string itemName, string latlng)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                return this.BadRequest("The item name must not be empty");
            }

            if (string.IsNullOrEmpty(latlng) || !(latlng.Contains(",")))
            {
                return this.BadRequest("The coordinates could not be parsed");
            }

            float lat, lng;

            if (!ParseLatLong(latlng, out lat, out lng))
            {
                return this.BadRequest("The coordinates could not be parsed");
            }

            var clothingItem = this._clothingItemFactory.GetClothingItem(GetClothingItemTypeFromString(itemName));

            var forecast = await this._forecastService.GetForecast(lat, lng);
            var isBeingWorn = clothingItem.IsBeingWorn(forecast);

            return this.Ok(isBeingWorn);    
        }

        private ClothingItemType GetClothingItemTypeFromString(string itemName)
        {
            switch (itemName.ToLowerInvariant())
            {
                case "sundress":
                    return ClothingItemType.Sundress;
                default:
                    return ClothingItemType.Invalid;
            }
        }

        private bool ParseLatLong(string latlng, out float lat, out float lng)
        {
            var latlngInArray = latlng.Split(',');
            var bothParsed = true;

            if (!float.TryParse(latlngInArray[0], out lat))
            {        
                bothParsed = false;
            }

            if (!float.TryParse(latlngInArray[1], out lng))
            {
                bothParsed = false;
            }

            return bothParsed;
        }
    }
}
