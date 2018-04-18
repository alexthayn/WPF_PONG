using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetBreakoutPongServer
{
    public class GameClient
    {
        TcpClient client;
        NetworkStream stream;
        byte[] inBuffer = new byte[1024];
        string data;
        string encoding;

        public GameClient(TcpListener server)
        {
            client = server.AcceptTcpClient();
        }

        public void SendData(ClassicPongGameData gameData)
        {
            stream = client.GetStream();
            encoding = JsonConvert.SerializeObject(gameData);
            stream.Write(Encoding.ASCII.GetBytes(encoding), 0, encoding.Length);
        }

        public Keypress GetKeypress()
        {
            stream = client.GetStream();
            int size = stream.Read(inBuffer, 0, inBuffer.Length);
            data = Encoding.ASCII.GetString(inBuffer, 0, size);
            return JsonConvert.DeserializeObject<Keypress>(data);
        }

        public bool RequestsContinue()
        {
            try
            {
                stream = client.GetStream();
                int size = stream.Read(inBuffer, 0, inBuffer.Length);
                data = Encoding.ASCII.GetString(inBuffer, 0, size);
                return JsonConvert.DeserializeObject<bool>(data);
            }
            catch (IOException e)
            {
                return false;
            }
        }

        public void ApproveContinue(bool approval)
        {
            stream = client.GetStream();
            encoding = JsonConvert.SerializeObject(approval);
            stream.Write(Encoding.ASCII.GetBytes(encoding), 0, encoding.Length);
        }

        public void Dispose()
        {
            stream.Close();
            client.Close();
        }
    }
}
