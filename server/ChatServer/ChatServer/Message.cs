using System;
using System.IO;
using System.Net.Sockets;

namespace ChatServer
{
    abstract class Message: IMessage
    {
        private IServer server;

        public Message(IServer server)
        {
            this.server = server;
        }

        protected static string NowStr()
        {
            return $"{DateTime.Now:dd\\/MM\\/yyyy h\\:mm tt}".Trim();
        }

        protected abstract string GetAdjustedMessage(string text, string username, string toUsername);

        public IMessage Send(string text, string username = "", string toUsername = "")
        {
            if (text.Trim() == "")
            {
                return this;
            }

            string msg = this.GetAdjustedMessage(text, username, toUsername);
            StreamWriter swSender;
            StatusEventArgs e = new(msg);
            ChatServer.Server.OnStatusChanged(e);

            TcpClient[] clients = new TcpClient[this.server.Users().Count];
            this.server.Users().Values.CopyTo(clients, 0);
            for (int i = 0; i < clients.Length; i++)
            {
                var client = clients[i];
                try
                {
                    if (client == null) { continue; }
                    if (toUsername != "")
                    {
                        string user = $"{this.server.Connections()[client]}";
                        if (user != toUsername) { continue; }
                    }

                    swSender = new StreamWriter(client.GetStream());
                    swSender.WriteLine(msg);
                    swSender.Flush();
                    swSender = null;
                }
                catch
                {
                    this.server.NicknameUnregister(client);
                }
            }
            return this;
        }

        public IServer Server()
        {
            return this.server;
        }
    }
}
