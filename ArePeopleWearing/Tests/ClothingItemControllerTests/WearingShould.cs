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
using System.Web.Http.Results;

namespace ArePeopleWearing.Tests.BeingWornControllerTests
{
    [TestFixture]
    public class GetShould
    {
        private ClothingItemController _controller;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var clothingItemFactory = new ClothingItemFactory();
            var forecastService = new Mock<IForecastService>();
            var forecastContext = new Mock<ForecastContext>();
            var forecastRepository = new Mock<ForecastRepository>(forecastContext.Object, forecastService.Object);
            var forecast = new Forecast()
            {
                MaximumTemperature = 80.0f,
                MinimumTemperature = 73.0f,
                CloudCover = 0.3f,
                PrecipitationProbability = 0.3f
            };

            var task = Task.FromResult(forecast);

            forecastRepository.Setup(s => s.GetForecast(-39.801f, 87.302f)).Returns(task);

            _controller = new ClothingItemController(clothingItemFactory, forecastRepository.Object);
        }

        [Test]
        public void ReturnBadRequestWhenItemNameIsEmpty()
        {
            var result = _controller.Wearing(string.Empty, "-39.801,87.302").Result;

            Assert.IsInstanceOf(typeof(BadRequestErrorMessageResult), result);
        }

        [Test]
        [TestCase("")]
        [TestCase("352342")]
        [TestCase("badlat,87.302")]
        [TestCase("-39.801,badlng")]
        [TestCase("badlat,badlng")]
        public async void ReturnBadRequestIfCoordinatesCannotBeParsed(string coordinates)
        {
            var result = await _controller.Wearing("sundress", coordinates);

            Assert.IsInstanceOf(typeof(BadRequestErrorMessageResult), result);
        }

        [Test]
        public async void ReturnTheForecastForAValidRequest()
        {
            var result = await _controller.Wearing("sundress", "-39.801,87.302");

            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<WearingClothingResult>), result);
        }
    }
}
