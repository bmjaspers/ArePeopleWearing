using ArePeopleWearing.Forecasts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Clothing.Tests.SundressTests
{
    [TestFixture]
    public class IsBeingWornShould
    {
        private static IClothingItem _sundress;
        private static Forecast _warmClearAndNoRain = new Forecast()
        {
                MinimumTemperature = 76,
                MaximumTemperature = 90,
                PrecipitationProbability = 0.15,
                CloudCover = 0.05
        };

        private static Forecast _warmOvercastAndNoRain = new Forecast()
        {
                MinimumTemperature = 76,
                MaximumTemperature = 90,
                PrecipitationProbability = 0.15,
                CloudCover = 0.7
         };

        private static Forecast _warmOvercastAndRainy = new Forecast()
        {
                MinimumTemperature = 76,
                MaximumTemperature = 90,
                PrecipitationProbability = 0.7,
                CloudCover = 0.7
        };

        private static Forecast _coldClearAndNoRain = new Forecast()
        {
            MinimumTemperature = 20,
            MaximumTemperature = 50,
            PrecipitationProbability = 0.7,
            CloudCover = 0.15
        };

        private static Forecast _coldOvercastAndNoRain = new Forecast()
        {
            MinimumTemperature = 20,
            MaximumTemperature = 50,
            PrecipitationProbability = 0.15,
            CloudCover = 0.41
        };

        private static Forecast _coldOvercastAndRainy = new Forecast()
        {
            MinimumTemperature = 20,
            MaximumTemperature = 50,
            PrecipitationProbability = 0.7,
            CloudCover = 0.41
        };

        private static Forecast[] _rainyForecasts = { _warmOvercastAndRainy, _coldOvercastAndRainy };
        private static Forecast[] _overcastForecasts = { _warmOvercastAndRainy, _coldOvercastAndRainy, _warmOvercastAndNoRain, _coldOvercastAndNoRain };
        private static Forecast[] _coldForecasts = { _coldClearAndNoRain, _coldOvercastAndNoRain, _coldOvercastAndRainy };

        [TestFixtureSetUp]
        public void SetUp()
        {
            _sundress = new Sundress();
        }

        [Test]
        public void ReturnYesWhenMinimumTemperatureGreaterThan75AndPrecipLessThan30AndCloudCoverLessThan40()
        {
            var result = _sundress.IsBeingWorn(_warmClearAndNoRain);

            Assert.AreEqual(WearingClothingResult.Yes, result);
        }

        [Test, TestCaseSource("_rainyForecasts")]
        public void ReturnNoWhenPrecipGreaterThan30(Forecast forecast)
        {
            var result = _sundress.IsBeingWorn(forecast);

            Assert.AreEqual(WearingClothingResult.No, result);
        }

        [Test, TestCaseSource("_overcastForecasts")]
        public void ReturnNoWhenOvercastGreaterThan40(Forecast forecast)
        {
            var result = _sundress.IsBeingWorn(forecast);

            Assert.AreEqual(WearingClothingResult.No, result);
        }

        [Test, TestCaseSource("_coldForecasts")]
        public void ReturnNoWhenMaximumTemperatureLessThan75(Forecast forecast)
        {
            var result = _sundress.IsBeingWorn(forecast);

            Assert.AreEqual(WearingClothingResult.No, result);
        }

        [Test]
        public void ReturnMaybeWhenMaximumTemperateGreaterThan75AndMinimumTemperatureGreaterThan50()
        {
            var forecast = new Forecast() { MaximumTemperature = 76, MinimumTemperature = 51 };
            var result = _sundress.IsBeingWorn(forecast);

            Assert.AreEqual(WearingClothingResult.Maybe, result);
        }
    }
}
