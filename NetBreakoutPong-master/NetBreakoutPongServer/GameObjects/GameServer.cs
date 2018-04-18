using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace NetBreakoutPong
{
    class GameServer
    {
        TcpClient client;
        NetworkStream stream;
        byte[] inBuffer = new byte[1024];
        string data;

        public GameServer()
        {
            TcpClient client = new TcpClient();
            client.Connect(Constants.SERVER_IP, Constants.SERVER_PORT);
        }

        public ClassicPongGameData GetData()
        {
            int size = stream.Read(inBuffer, 0, inBuffer.Length);
            string message = Encoding.ASCII.GetString(inBuffer, 0, size);
            Console.Write("Received Message:\n" + message);
            return JsonConvert.DeserializeObject<ClassicPongGameData>(message);
        }

        /* This isn't right, first we need to figure out how to package client to server data.
        public void SendData(ClassicPongGameData gameData)
        {
            string encoding = JsonConvert.SerializeObject(gameData);
            Console.Write("Sent Message:\n" + encoding);
            stream.Write(Encoding.ASCII.GetBytes(encoding), 0, encoding.Length);
        }
        */

    }
}
