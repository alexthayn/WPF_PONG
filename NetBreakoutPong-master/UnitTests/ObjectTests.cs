using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetBreakoutPongServer;

namespace UnitTests
{
    [TestClass]
    public class ObjectTests
    {
        [TestMethod]
        public void BallTest1()
        {
            Ball myBall = new Ball(new Location(5000, 5000), (float)97.3, 400, 20);

            myBall.Move();
            myBall.Move();

            Location finalLocation = myBall.Position;
            Location expectedLocation = new Location(4206, 5102);

            Assert.AreEqual(expectedLocation.X, finalLocation.X, 2);
            Assert.AreEqual(expectedLocation.Y, finalLocation.Y, 2);
        }

        [TestMethod]
        public void BallTest2()
        {
            Ball myBall = new Ball(new Location(5000, 5000), (float)310, 856, 20);

            myBall.Move();

            myBall.Direction = (float)97.3;
            myBall.Velocity = 400;

            myBall.Move();

            Location finalLocation = myBall.Position;
            Location expectedLocation = new Location(5259, 4501);

            Assert.AreEqual(expectedLocation.X, finalLocation.X, 2);
            Assert.AreEqual(expectedLocation.Y, finalLocation.Y, 2);
        }

        [TestMethod]
        public void PaddleTest1()
        {
            Paddle myPaddle = new Paddle(new Location(5000, 20), 600, 50, 200);

            myPaddle.MovingWest = true;
            myPaddle.Move();

            Location finalLocation = myPaddle.Position;
            Location expectedLocation = new Location(4800, 20);

            Assert.AreEqual(expectedLocation.X, finalLocation.X, 2);
            Assert.AreEqual(expectedLocation.Y, finalLocation.Y, 2);
        }

        [TestMethod]
        public void PaddleTest2()
        {
            Paddle myPaddle = new Paddle(new Location(5000, 20), 600, 50, 200);

            myPaddle.MovingWest = true;
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Move();

            Location finalLocation = myPaddle.Position;
            Location expectedLocation = new Location(4200, 20);

            Assert.AreEqual(expectedLocation.X, finalLocation.X, 2);
            Assert.AreEqual(expectedLocation.Y, finalLocation.Y, 2);
        }

        [TestMethod]
        public void PaddleTest3()
        {
            Paddle myPaddle = new Paddle(new Location(5000, 20), 600, 50, 200);

            myPaddle.MovingWest = true;
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Velocity += 100;
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Velocity = 1000;
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Move();

            Location finalLocation = myPaddle.Position;
            Location expectedLocation = new Location(myPaddle.Width/2, 20);

            Assert.AreEqual(expectedLocation.X, finalLocation.X, 2);
            Assert.AreEqual(expectedLocation.Y, finalLocation.Y, 2);
        }

        [TestMethod]
        public void PaddleTest4()
        {
            Paddle myPaddle = new Paddle(new Location(5000, 20), 600, 50, 200);

            myPaddle.MovingWest = true;
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Velocity += 100;
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Velocity = 1000;
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Move();
            myPaddle.Velocity = 700;
            myPaddle.Move();

            Location finalLocation = myPaddle.Position;
            Location expectedLocation = new Location(myPaddle.Width / 2, 20);

            Assert.AreEqual(expectedLocation.X, finalLocation.X, 2);
            Assert.AreEqual(expectedLocation.Y, finalLocation.Y, 2);
        }

    }
}
