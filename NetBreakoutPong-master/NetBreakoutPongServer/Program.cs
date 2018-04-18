using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetBreakoutPongServer
{
    class Program
    {
        static Keypress POneKP;
        static Keypress PTwoKP;
        static bool pTwoContinue;
        static bool pOneContinue;
        static ClassicPongGame game;
        static GameClient PlayerOne;
        static GameClient PlayerTwo;

        static void Main(string[] args)
        {
            Console.WriteLine("Pong Server is starting up...");
            // Setup as server here
            IPAddress ipAd = IPAddress.Parse(Constants.SERVER_IP);
            TcpListener server = new TcpListener(ipAd, Constants.SERVER_PORT);
            server.Start();

            while (true)
            {
                // Connect Two Clients
                Console.WriteLine("Waiting for Player One to connect...");
                PlayerOne = new GameClient(server);
                Console.WriteLine("Waiting for Player Two to connect...");
                PlayerTwo = new GameClient(server);

                try
                {
                    do
                    {
                        Console.WriteLine("Generating a Classic Pong Game...");
                        game = new ClassicPongGame();

                        // Send initial locations
                        Console.WriteLine("Ready...");
                        PlayerOne.SendData(game.GetDataForP1());
                        PlayerTwo.SendData(game.GetDataForP2());


                        // wait for a sec before starting
                        Console.WriteLine("Set...");
                        Thread.Sleep(1000);

                        // As long as the game is not over, keep playing.
                        Console.WriteLine("Go!");
                        while (!game.PlayerOneLost() && !game.PlayerTwoLost())
                        {
                            // Move and update everything for player one
                            POneKP = PlayerOne.GetKeypress();
                            game.POneUpdate(POneKP);
                            PlayerOne.SendData(game.GetDataForP1());
                            Thread.Sleep(Constants.SLEEP_DELAY);

                            // Move and update everything for player two
                            PTwoKP = PlayerTwo.GetKeypress();
                            game.PTwoUpdate(PTwoKP);
                            PlayerTwo.SendData(game.GetDataForP2());
                            Thread.Sleep(Constants.SLEEP_DELAY);
                        }

                        // Throw away these keypresses. Nobody loves them.
                        PlayerOne.GetKeypress();
                        PlayerTwo.GetKeypress();

                        // Send final positions with gameOverInfo
                        // PlayerOne
                        ClassicPongGameData temp = game.GetDataForP1();
                        temp.ILost = game.PlayerOneLost();
                        temp.OppLost = game.PlayerTwoLost();
                        PlayerOne.SendData(temp);
                        // PlayerTwo
                        temp = game.GetDataForP2();
                        temp.ILost = game.PlayerTwoLost();
                        temp.OppLost = game.PlayerOneLost();
                        PlayerTwo.SendData(temp);

                        pOneContinue = PlayerOne.RequestsContinue();
                        pTwoContinue = PlayerTwo.RequestsContinue();

                        PlayerOne.ApproveContinue(pOneContinue && pTwoContinue);
                        PlayerTwo.ApproveContinue(pOneContinue && pTwoContinue);

                    } while (pOneContinue && pTwoContinue);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Connection with client(s) was interrupted unexpectedly, restarting game.");
                }

                Thread.Sleep(100);

                if (PlayerOne != null)
                    PlayerOne.Dispose();

                if (PlayerTwo != null)
                    PlayerTwo.Dispose();
            }
        }
    }
}
