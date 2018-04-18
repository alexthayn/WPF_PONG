namespace NetBreakoutPongServer
{
    public class Paddle : IPaddle
    {
        private Location position;

        public Location Position {
            get
            {
                return position;
            }
        }

        public long Velocity { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
        private bool movingEast = false;
        private bool movingWest = false;

        public bool MovingEast
        {
            get
            {
                return movingEast;
            }
            
            set
            {
                movingWest = false;
                movingEast = value;
            }
        }
        public bool MovingWest
        {
            get
            {
                return movingWest;
            }

            set
            {
                movingEast = false;
                movingWest = value;
            }
        }

        public Paddle(Location startCenter, long width, long height, long velocity)
        {
            this.position = startCenter;
            this.Width = width;
            this.Height = height;
            this.Velocity = velocity;
        }


        private void MoveEast()
        {
            if (position.X + this.Width / 2 < Constants.PLAYFIELD_WIDTH - Velocity)
            {
                position.X += Velocity;
            }
            else if ((position.X + this.Width / 2 < Constants.PLAYFIELD_WIDTH))
            {
                position.X = Constants.PLAYFIELD_WIDTH - this.Width / 2;
            }
        }

        private void MoveWest()
        {
            if (position.X - this.Width / 2 > Velocity)
            {
                position.X -= Velocity;
            }
            else if ((position.X - this.Width / 2 > 0))
            {
                position.X = this.Width / 2;
            }
        }

        public void SetStationary()
        {
            movingEast = movingWest = false;
        }

        public void Move()
        {
            if (movingWest)
                MoveWest();
            else if (movingEast)
                MoveEast();
        }
    }
}
