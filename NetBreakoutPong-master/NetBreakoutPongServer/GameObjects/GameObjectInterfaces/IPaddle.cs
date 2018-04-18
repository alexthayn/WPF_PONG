namespace NetBreakoutPongServer
{
    interface IPaddle
    {
        Location Position { get; }
        long Velocity { set; get; }
        long Width { set; get; }
        long Height { set; get; }
        bool MovingEast { set; get; }
        bool MovingWest { set; get; }
        
        void Move();
    }
}
