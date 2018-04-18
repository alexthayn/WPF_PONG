using System;

namespace NetBreakoutPongServer
{
    public class Vector
    {

        public Vector(long velocity, float direction)
        {
            Velocity = velocity;
            Direction = direction;
        }
        double deltaX = 10000;
        double deltaY;

        public long DeltaX
        {
            get { return (long)deltaX; }
            set { deltaX = value; }
        }
        public long DeltaY
        {
            get { return (long)deltaY; }
            set { deltaY = value; }
        }

        public float Direction
        {
            get
            {
                return (float)(Math.Atan2(DeltaY, DeltaX) * 180.0 / Math.PI);
            }

            set
            {
                var velocity = Velocity;
                deltaX = (long)(Math.Cos(value * Math.PI / 180.0) * velocity);
                deltaY = (long)(Math.Sin(value * Math.PI / 180.0) * velocity);
            }
        }
        public long Velocity
        {
            get
            {
                return (long)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            }
            set
            {
                var direction = Direction;
                deltaX = (long)(Math.Cos(direction * Math.PI / 180.0) * value);
                deltaY = (long)(Math.Sin(direction * Math.PI / 180.0) * value);
            }
        }

        public void Move(ref Location center)
        {
            center.X += DeltaX;
            center.Y += DeltaY;
        }

        public void InvertDeltaX()
        {
            deltaX = 0 - deltaX;
        }

        public void InvertDeltaY()
        {
            deltaY = 0 - deltaY;
        }
    }
}