using ArePeopleWearing.Forecasts.Services;
using ForecastIO;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts.Tests.Services.ForecastIOServiceTests
{
    [TestFixture]
    public class GetForecastShould
    {
        private ForecastIOService _service;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var client = new Mock<ForecastIOClient>("apikey");

            var response = new ForecastIOResponse();
            response.daily = new Daily();
            response.daily.data = new List<DailyForecast>();
            response.daily.data.Add(
                    new DailyForecast()
                    {
                        temperatureMin = 20.0f,
                        temperatureMax = 50.0f,
                        cloudCover = 0.3f,
                        precipProbability = 0.3f
                    }
            );

            var task = Task.FromResult(response);

            client.Setup(c => c.GetForecastResponse(-39.801f, 87.333f)).Returns(task);
            var responseMapper = new ForecastIOResponseMapper();

            _service = new ForecastIOService(client.Object, responseMapper);
        }

        [Test]
        public void ReturnTheMappedForecast()
        {
            var forecast = _service.GetForecast(-39.801f, 87.333f).Result;

            Assert.AreEqual(20.0f, forecast.MinimumTemperature);
            Assert.AreEqual(0.3f, forecast.PrecipitationProbability);
            Assert.AreEqual(0.3f, forecast.CloudCover);
            Assert.AreEqual(50.0f, forecast.MaximumTemperature);
        }
    }
}
