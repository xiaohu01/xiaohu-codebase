using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarFinder;

namespace CarFinderTest
{
    [TestClass]
    public class CarFinderTest
    {
        [TestMethod]
        public void CarFinderShouldHaveRightName()
        {
            // arrange
            Car car = new Car();
            CarFinderBase binaryCarFinder = new BinaryCarFinder(car);
            CarFinderBase goldenSectionCarFinder = new GoldenSectionCarFinder(car);

            // act

            // assert
            Assert.AreEqual(binaryCarFinder.FinderName, "BinaryCarFinder");
            Assert.AreEqual(goldenSectionCarFinder.FinderName, "GoldenSectionCarFinder");
        }
    }
}
