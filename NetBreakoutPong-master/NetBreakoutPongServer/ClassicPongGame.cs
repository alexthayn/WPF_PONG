using System;

namespace NetBreakoutPongServer
{
    public class ClassicPongGame
    {
        Ball gameBall;
        Paddle playerOne;
        Paddle playerTwo;

        public ClassicPongGame()
        {
            // Place ball, and assign random direction
            gameBall = new Ball(new Location(Constants.PLAYFIELD_WIDTH / 2, Constants.PLAYFIELD_HEIGHT / 2), new Random().Next(0, 359), Constants.ClassicPong.BALL_VELOCITY, Constants.ClassicPong.BALL_RADIUS);

            // Place Paddle of player one on south wall.
            playerOne = new Paddle(new Location(Constants.PLAYFIELD_WIDTH / 2, 0), Constants.ClassicPong.PADDLE_WIDTH, Constants.ClassicPong.PADDLE_HEIGHT, Constants.ClassicPong.PADDLE_VELOCITY);

            // Place paddle of player two on north wall.
            playerTwo = new Paddle(new Location(Constants.PLAYFIELD_WIDTH / 2, Constants.PLAYFIELD_HEIGHT), Constants.ClassicPong.PADDLE_WIDTH, Constants.ClassicPong.PADDLE_HEIGHT, Constants.ClassicPong.PADDLE_VELOCITY);
        }

        public void POneUpdate(Keypress POneKP)
        {
            // Move paddles first, 'cause I just got the args

            // PlayerOnePaddle
            if (POneKP.left && !POneKP.right)
                playerOne.MovingWest = true;
            else if (POneKP.right && !POneKP.left)
                playerOne.MovingEast = true;
            else if (!POneKP.left && !POneKP.right)
                playerOne.SetStationary();
            else
                playerOne.SetStationary();
            playerOne.Move();
            playerTwo.Move();

            // Move ball
            gameBall.Move();
            TestCollisions();
            gameBall.Velocity += Constants.ClassicPong.BALL_SPEEDUP;
        }

        public void PTwoUpdate(Keypress PTwoKP)
        {
            // Move paddles first, 'cause I just got the args

            // PlayerTwoPaddle
            if (PTwoKP.left && !PTwoKP.right)
                playerTwo.MovingEast = true;
            else if (PTwoKP.right && !PTwoKP.left)
                playerTwo.MovingWest = true;
            else if (!PTwoKP.left && !PTwoKP.right)
                playerTwo.SetStationary();
            else
                playerTwo.SetStationary();

            playerTwo.Move();
            playerOne.Move();

            // Move ball
            gameBall.Move();
            TestCollisions();
            gameBall.Velocity += Constants.ClassicPong.BALL_SPEEDUP;
        }

        public bool PlayerOneLost()
        {
            return gameBall.HitSouthWall();
        }

        public bool PlayerTwoLost()
        {
            return gameBall.HitNorthWall();
        }

        public ClassicPongGameData GetDataForP1()
        {
            ClassicPongGameData P1Data = new ClassicPongGameData();

            // store gameBall data
            P1Data.gameBall.Position = new Location(gameBall.Position.X / 1000, (Constants.PLAYFIELD_HEIGHT - gameBall.Position.Y) / 1000);
            P1Data.gameBall.Radius = (gameBall.Radius / 1000);

            // store POne data
            P1Data.myPaddle.Height = playerOne.Height / 1000;
            P1Data.myPaddle.Width = playerOne.Width / 1000;
            P1Data.myPaddle.Position = new Location(playerOne.Position.X / 1000, (Constants.PLAYFIELD_HEIGHT - playerOne.Position.Y) / 1000);

            // store PTwo data
            P1Data.oppPaddle.Height = playerTwo.Height / 1000;
            P1Data.oppPaddle.Width = playerTwo.Width / 1000;
            P1Data.oppPaddle.Position = new Location(playerTwo.Position.X / 1000, (Constants.PLAYFIELD_HEIGHT - playerTwo.Position.Y) / 1000);

            // Set game not over, driver will take care of this
            P1Data.OppLost = P1Data.ILost = false;

            return P1Data;
        }

        public ClassicPongGameData GetDataForP2()
        {
            ClassicPongGameData P2Data = new ClassicPongGameData();

            // store gameBall data
            P2Data.gameBall.Position = new Location((Constants.PLAYFIELD_WIDTH - gameBall.Position.X) / 1000, gameBall.Position.Y / 1000);
            P2Data.gameBall.Radius = (gameBall.Radius / 1000);

            // store PTwo data
            P2Data.myPaddle.Height = playerTwo.Height / 1000;
            P2Data.myPaddle.Width = playerTwo.Width / 1000;
            P2Data.myPaddle.Position = new Location((Constants.PLAYFIELD_WIDTH - playerTwo.Position.X) / 1000, playerTwo.Position.Y / 1000);

            // store POne data
            P2Data.oppPaddle.Height = playerOne.Height / 1000;
            P2Data.oppPaddle.Width = playerOne.Width / 1000;
            P2Data.oppPaddle.Position = new Location((Constants.PLAYFIELD_WIDTH - playerOne.Position.X) / 1000, playerOne.Position.Y / 1000);

            // Store game over info
            P2Data.OppLost = P2Data.ILost = false;

            return P2Data;
        }

        private void TestCollisions()
        {
            gameBall.TestCollision(playerOne);
            gameBall.TestCollision(playerTwo);
        }

    }
}