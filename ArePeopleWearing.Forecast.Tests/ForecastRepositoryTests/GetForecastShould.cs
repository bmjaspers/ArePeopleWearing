using ArePeopleWearing.Forecasts.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Forecasts.Tests.ForecastRepositoryTests
{
    [TestFixture]
    public class GetForecastShould
    {
        [Test]
        public async void ReturnTheForecastFromTheDatabaseIfItExists()
        {
            var forecastRepository = GetForecastRepositoryWithForecastsInContext();

            var forecast = await forecastRepository.GetForecast(39.801f, -87.901f);
            Assert.AreEqual(50.0f, forecast.MaximumTemperature);
            Assert.AreEqual(39.801f, forecast.Latitude);
            Assert.AreEqual(-87.901f, forecast.Longitude);
        }

        [Test]
        public async void ReturnTheForecastFromTheServiceIfItIsNotInTheDatabase()
        {
            var forecastRepository = GetForecastRepositoryWithForecastsInContextAndService();

            var forecast = await forecastRepository.GetForecast(39.801f, -87.901f);
            Assert.AreEqual(50.0f, forecast.MaximumTemperature);
            Assert.AreEqual(39.801f, forecast.Latitude);
            Assert.AreEqual(-87.901f, forecast.Longitude);
        }

        private ForecastRepository GetForecastRepositoryWithForecastsInContextAndService()
        {
            var data = new List<Forecast> 
            { 
                new Forecast { Latitude = 39.801f, Longitude = -87.901f, Date = DateTime.Now, MaximumTemperature = 50.0f },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Forecast>>();

            mockSet.As<IDbAsyncEnumerable<Forecast>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Forecast>(data.GetEnumerator()));

            mockSet.As<IQueryable<Forecast>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Forecast>(data.Provider));

            mockSet.As<IQueryable<Forecast>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Forecast>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Forecast>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var forecastContext = new Mock<ForecastContext>();
            forecastContext.Setup(c => c.Forecasts).Returns(mockSet.Object);

            var forecastService = new Mock<IForecastService>();
            var forecast = new Forecast { Latitude = 37.801f, Longitude = -87.901f, Date = DateTime.Now, MaximumTemperature = 50.0f };
            var task = Task.FromResult(forecast);

            forecastService.Setup(s => s.GetForecast(37.801f, -87.901f)).Returns(task);
            var forecastRepository = new ForecastRepository(forecastContext.Object, forecastService.Object);

            return forecastRepository;
        }

        private ForecastRepository GetForecastRepositoryWithForecastsInContext()
        {
            var data = new List<Forecast> 
            { 
                new Forecast { Latitude = 39.801f, Longitude = -87.901f, Date = DateTime.Now, MaximumTemperature = 50.0f },
                new Forecast { Latitude = 37.801f, Longitude = -87.901f, Date = DateTime.Now, MaximumTemperature = 50.0f },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Forecast>>();

            mockSet.As<IDbAsyncEnumerable<Forecast>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Forecast>(data.GetEnumerator()));

            mockSet.As<IQueryable<Forecast>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Forecast>(data.Provider)); 

            mockSet.As<IQueryable<Forecast>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Forecast>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Forecast>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var forecastContext = new Mock<ForecastContext>();
            forecastContext.Setup(c => c.Forecasts).Returns(mockSet.Object);

            var forecastService = new Mock<IForecastService>();
            var forecastRepository = new ForecastRepository(forecastContext.Object, forecastService.Object);

            return forecastRepository;
        }
    }
}
