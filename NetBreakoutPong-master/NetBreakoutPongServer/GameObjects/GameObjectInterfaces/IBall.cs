namespace NetBreakoutPongServer
{
    interface IBall
    {
        Location Position { get; }
        long Radius { set; get; }
        float Direction { get; set; }
        long Velocity { get; set; }
        int IdLastPaddle { get; set; }
        void Move();
    }
}
