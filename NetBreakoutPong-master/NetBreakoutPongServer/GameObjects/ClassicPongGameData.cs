namespace NetBreakoutPongServer
{
    public struct ClassicPongGameData
    {
        public bool ILost;
        public bool OppLost;

        public BallData gameBall;

        public PaddleData myPaddle;
        public PaddleData oppPaddle;


        public struct PaddleData
        {
            public Location Position;
            public long Width;
            public long Height;
        }

        public struct BallData
        {
            public Location Position;
            public long Radius;
        }
        
    }
}
