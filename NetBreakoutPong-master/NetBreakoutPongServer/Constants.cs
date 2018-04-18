namespace NetBreakoutPongServer
{
    public static class Constants
    {
        public const int SCREEN_WIDTH = 500;
        public const int SCREEN_HEIGHT = 600;

        public const int PLAYFIELD_WIDTH = SCREEN_WIDTH * 1000;
        public const int PLAYFIELD_HEIGHT = SCREEN_HEIGHT * 1000;

        public const string SERVER_IP = "172.31.7.253";
        public const int SERVER_PORT = 5555;

        public const int SLEEP_DELAY = 25;

        public static class ClassicPong
        {
            public const int PADDLE_WIDTH = 80000;
            public const int PADDLE_HEIGHT = 10000;
            public const int BALL_RADIUS = 10000;
            public const int BALL_VELOCITY = 5000;
            public const int PADDLE_VELOCITY = 7000;
            public const int BALL_SPEEDUP = 3;
        }


    }
}