using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetBreakoutPongServer;
using NetBreakoutPongClient;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace UnitTests
{
    [TestClass]
    public class NetworkingTests
    {
        [TestMethod]
        public void NetTest1()
        {
            Thread serverThread = new Thread(DoServerStuff);

            serverThread.Start();
            //now being a client
            NetBreakoutPongClient.GameServer server = new NetBreakoutPongClient.GameServer();

            NetBreakoutPongClient.ClassicPongGameData resultData = server.GetData();

            NetBreakoutPongClient.ClassicPongGameData expectedData = new NetBreakoutPongClient.ClassicPongGameData();
            expectedData.gameBall.Position = new NetBreakoutPongClient.Location(250, 300);
            expectedData.gameBall.Radius = 50;
            expectedData.myPaddle.Height = 40;
            expectedData.myPaddle.Width = 150;
            expectedData.myPaddle.Position = new NetBreakoutPongClient.Location(500, 580);
            expectedData.oppPaddle.Height = 40;
            expectedData.oppPaddle.Width = 150;
            expectedData.oppPaddle.Position = new NetBreakoutPongClient.Location(300, 20);
            serverThread.Join();

            server.Dispose();

            Assert.AreEqual(expectedData.myPaddle.Position.X, resultData.myPaddle.Position.X);
        }

        [TestMethod]
        public void NetTest2()
        {
            Thread serverThread = new Thread(DoServerStuff);

            serverThread.Start();

            NetBreakoutPongClient.GameServer server = new NetBreakoutPongClient.GameServer();

            NetBreakoutPongClient.ClassicPongGameData resultData = server.GetData();

            NetBreakoutPongClient.ClassicPongGameData expectedData = new NetBreakoutPongClient.ClassicPongGameData();
            expectedData.gameBall.Position = new NetBreakoutPongClient.Location(250, 300);
            expectedData.gameBall.Radius = 50;
            expectedData.myPaddle.Height = 40;
            expectedData.myPaddle.Width = 150;
            expectedData.myPaddle.Position = new NetBreakoutPongClient.Location(500, 580);
            expectedData.oppPaddle.Height = 40;
            expectedData.oppPaddle.Width = 150;
            expectedData.oppPaddle.Position = new NetBreakoutPongClient.Location(300, 20);

            serverThread.Join();
            server.Dispose();

            // Assert.AreEqual(false, resultData.WinnerPlayerOne); // Fix later.. maybe
        }

        void DoServerStuff()
        {
            // Setup as server here
            IPAddress ipAd = IPAddress.Parse(NetBreakoutPongServer.Constants.SERVER_IP);
            TcpListener server = new TcpListener(ipAd, NetBreakoutPongServer.Constants.SERVER_PORT);
            server.Start();

            NetBreakoutPongServer.ClassicPongGameData data = new NetBreakoutPongServer.ClassicPongGameData();
            data.gameBall.Position = new NetBreakoutPongServer.Location(250, 300);
            data.gameBall.Radius = 50;
            data.myPaddle.Height = 40;
            data.myPaddle.Width = 150;
            data.myPaddle.Position = new NetBreakoutPongServer.Location(500, 580);
            data.oppPaddle.Height = 40;
            data.oppPaddle.Width = 150;
            data.oppPaddle.Position = new NetBreakoutPongServer.Location(300, 20);

            NetBreakoutPongServer.GameClient client = new NetBreakoutPongServer.GameClient(server);

            //Thread.Sleep(5000);

            client.SendData(data);

            client.Dispose();
        }
        


    }
}
