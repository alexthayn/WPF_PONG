using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetBreakoutPongServer;

namespace UnitTests
{
    [TestClass]
    public class ObjectElementTests
    {
        [TestMethod]
        public void VectorTest1()
        {
            Vector myVector = new Vector(1000, 0);

            Location myLocation = new Location(5000, 5000);

            myVector.Move(ref myLocation);

            Assert.AreEqual(4000, myLocation.Y);
            Assert.AreEqual(5000, myLocation.X);
        }

        [TestMethod]
        public void VectorTest2()
        {
            Vector myVector = new Vector(1000, 90);

            Location myLocation = new Location(5000, 5000);

            myVector.Move(ref myLocation);

            Assert.AreEqual(5000, myLocation.Y);
            Assert.AreEqual(4000, myLocation.X);
        }

        [TestMethod]
        public void VectorTest3()
        {
            Vector myVector = new Vector(1000, 180);

            Location myLocation = new Location(5000, 5000);

            myVector.Move(ref myLocation);

            Assert.AreEqual(6000, myLocation.Y);
            Assert.AreEqual(5000, myLocation.X);
        }

        [TestMethod]
        public void VectorTest4()
        {
            Vector myVector = new Vector(1000, 270);

            Location myLocation = new Location(5000, 5000);

            myVector.Move(ref myLocation);

            Assert.AreEqual(5000, myLocation.Y);
            Assert.AreEqual(6000, myLocation.X);
        }

        [TestMethod]
        public void VectorTest5()
        {
            Vector myVector = new Vector(1000, 45);

            Location myLocation = new Location(5000, 5000);

            myVector.Move(ref myLocation);

            Assert.AreEqual(4293, myLocation.Y);
            Assert.AreEqual(4293, myLocation.X);
        }

        [TestMethod]
        public void VectorTest6()
        {
            Vector myVector = new Vector(1000, 60);

            Location myLocation = new Location(5000, 5000);

            myVector.Move(ref myLocation);

            Assert.AreEqual(4500, myLocation.Y);
            Assert.AreEqual(4134, myLocation.X);
        }

        [TestMethod]
        public void VectorTest7()
        {
            Vector myVector = new Vector(1000, (float)196.7);

            Location myLocation = new Location(5000, 5000);

            myVector.Move(ref myLocation);

            Assert.AreEqual(5958, myLocation.Y, 1);
            Assert.AreEqual(5287, myLocation.X, 1);
        }
    }
}
