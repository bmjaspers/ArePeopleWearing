using ArePeopleWearing.Clothing;
using ArePeopleWearing.Controllers;
using ArePeopleWearing.Forecasts;
using ArePeopleWearing.Forecasts.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ArePeopleWearing.Tests.BeingWornControllerTests
{
    [TestFixture]
    public class GetShould
    {
        private BeingWornController _controller;

        public void SetUp()
        {
            var clothingItemFactory = new ClothingItemFactory();
            var forecastService = new Mock<IForecastService>();
            var forecast = new Forecast()
            {
                MaximumTemperature = 80.0,
                MinimumTemperature = 73.0
            };

            forecastService.Setup(s => s.GetForecast(-39.801f, 87.302f)).Returns(forecast);

            _controller = new BeingWornController(clothingItemFactory, forecastService.Object);
        }

        public void ReturnBadRequestWhenItemNameIsNullOrEmpty()
        {
            var result = _controller.Get(string.Empty, "-39.801,87.302");

            Assert.IsInstanceOf(typeof(System.Web.Http.))
        }
    }
}
