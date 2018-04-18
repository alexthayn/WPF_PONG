namespace NetBreakoutPongServer
{
    public class Ball : IBall
    {
        private Vector ballVector;
        private Location position;
        public Location Position { get { return position; } }
        public long Radius { get; set; }
        public int IdLastPaddle { get; set; }

        public Ball(Location startPosition, float startDirection, long startVelocity, long radius)
        {
            this.position = startPosition;
            this.ballVector = new Vector(startVelocity, startDirection);
            this.Radius = radius;
        }

        public void TestCollision(Ball otherBall)
        {
            long totalDistance = this.Radius + otherBall.Radius;
            long yDiff = this.Position.Y - otherBall.Position.Y;
            long xDiff = this.Position.X - otherBall.Position.X;
            if (totalDistance * totalDistance >= xDiff*xDiff + yDiff*yDiff)
            {
                long thisD = this.ballVector.DeltaY;
                this.ballVector.DeltaY = otherBall.ballVector.DeltaY;
                otherBall.ballVector.DeltaY = thisD;

                thisD = this.ballVector.DeltaX;
                this.ballVector.DeltaX = otherBall.ballVector.DeltaX;
                otherBall.ballVector.DeltaX = thisD;
            }
        }

        public void TestCollision(Paddle paddle)
        {
            long northReflectLine = paddle.Position.Y + paddle.Height/2 + this.Radius;
            long southReflectLine = paddle.Position.Y - paddle.Height/2 - this.Radius;

            long eastReflectLine = paddle.Position.X + paddle.Width/2 + this.Radius;
            long westReflectLine = paddle.Position.X - paddle.Width/2 - this.Radius;

            if (southReflectLine <= this.Position.Y && this.Position.Y <= northReflectLine)
            {
                if (westReflectLine <= this.Position.X && this.Position.X <= eastReflectLine)
                {
                    ballVector.InvertDeltaY();
                }
            }
        }

        public bool HitNorthWall()
        {
            if (Position.Y > Constants.PLAYFIELD_HEIGHT)
                return true;
            return false;
        }

        public bool HitSouthWall()
        {
            if (Position.Y < 0)
                return true;
            return false;
        }

        public float Direction
        {
            set
            {
                ballVector.Direction = value;
            }
            get
            {
                return ballVector.Direction;
            }
        }

        public long Velocity
        {
            set
            {
                ballVector.Velocity = value;
            }
            get
            {
                return ballVector.Velocity;
            }
        }

        public void Move()
        {
            ballVector.Move(ref position);
            if (position.X < 0 || position.X > Constants.PLAYFIELD_WIDTH)
                ballVector.InvertDeltaX();
        }


    }
}
