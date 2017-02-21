using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarFinder;

namespace CarFinderTest
{
    [TestClass]
    public class CarTest
    {
        [TestMethod]
        public void CarShouldHaveSpecifiedInitialPositionAndVelocity()
        {
            // arrange
            int initPosition = 387;
            int velocity = -895;
            Car car = new Car(initPosition,velocity);

            // act


            // assert
            Assert.AreEqual(initPosition, car.InitialPosition);
            Assert.AreEqual(velocity, car.Velocity);
        }

        [TestMethod]
        public void CarInitialPositionShouldBeInValidRange()
        {
            // arrange
            Car car = new Car();

            // act
            car.Start();
            car.Stop();

            // assert
            Assert.IsTrue(car.Position >= -1000 && car.Position <= 1000);
        }

        [TestMethod]
        public void CarShouldHaveNonZeroVelocityIfNotSpecified()
        {
            // arrange
            Car car = new Car();

            // act


            // assert
            Assert.IsTrue(car.Velocity != 0);
        }

        [TestMethod]
        public void CarShouldMoveAfterStarted()
        {
            // arrange
            bool moving = false;
            Car car = new Car();
            car.moved += (obj, e) => moving = true;

            // act
            car.Start();
            System.Threading.Thread.Sleep(10);
            car.Stop();

            // assert
            Assert.IsTrue(moving);
        }
    }
}
