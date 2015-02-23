using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArePeopleWearing.Clothing.Tests.ClothingItemFactoryTests
{
    [TestFixture]
    public class GetClothingItemShould
    {
        private ClothingItemFactory _factory;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _factory = new ClothingItemFactory();
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void ThrowANotImplementedExceptionWhenClothingItemTypeIsInvalid()
        {
            _factory.GetClothingItem(ClothingItemType.Invalid);
        }

        [Test]
        public void ReturnASundressForClothingItemTypeSundress()
        {
            var sundress = _factory.GetClothingItem(ClothingItemType.Sundress);

            Assert.IsTrue(typeof(Sundress) == sundress.GetType());
        }
    }
}
