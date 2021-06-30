using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ChatServer
{
    // Define parameters to event.
    public delegate void StatusEventHandler(object sender, StatusEventArgs e);

    class Server: IServer
    {
        private IServerProperties props;
        private Hashtable users;
        private Hashtable connections;
        private TcpClient tcpClient;
        public static event StatusEventHandler StatusChanged;
        private Thread listener;
        private TcpListener clientTcp;
        private bool isRunning = false;

        public static IServer New(IServerProperties props, StatusEventHandler statusEvent)
        {
            return new Server(props, statusEvent);
        }

        public Server(IServerProperties props, StatusEventHandler statusEvent)
        {
            this.props = props;
            this.users = new Hashtable(this.props.MaxUsersNumbers());
            this.connections = new Hashtable(50);
            Server.StatusChanged += statusEvent;
        }

        public IServer Run()
        {
            try
            {
                this.clientTcp = new TcpListener(this.props.Host(), this.props.Port());
                this.clientTcp.Start();
                this.isRunning = true;
                this.listener = new Thread(this.AddListener);
                this.listener.IsBackground = true;
                this.listener.Start();
                System.Console.WriteLine(this.props.ToString());
            }
            catch
            {

            }
            return this;
        }

        private void AddListener()
        {
            while (this.isRunning)
            {
                this.tcpClient = this.clientTcp.AcceptTcpClient();
                Connection conn = new(this.tcpClient, this);
            }
        }

        protected static string NowStr()
        {
            return $"{DateTime.Now:dd\\/ MM\\/ yyyy h\\:mm tt}";
        }

        public bool NicknameRegister(TcpClient tcpConn, string username)
        {
            bool notContains = !this.users.Contains(username);
            if (notContains)
            {
                this.users.Add(username, tcpConn);
                this.connections.Add(tcpConn, username);
                this.SendMsgAdmin($"[USER REGISTER] The user {username} has joined us at {Server.NowStr()}.");
            }
            return notContains;
        }

        public IServer NicknameUnregister(TcpClient tcpConn)
        {
            if (this.connections[tcpConn] != null)
            {
                this.SendMsgAdmin($"[USER UNREGISTER] The user {this.connections[tcpConn]} has logged out at {Server.NowStr()}.");
                this.users.Remove(this.connections[tcpConn]);
                this.connections.Remove(tcpConn);
            }
            return this;
        }

        public static void OnStatusChanged(StatusEventArgs e)
        {
            StatusEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                statusHandler(null, e);
            }
        }

        public IServer SendMsgAdmin(string msg)
        {
            if (msg.Trim() == "")
            {
                return this;
            }
            this.InternalSendMessage($"@admin {Server.NowStr()}: {msg}");
            return this;
        }

        public IServer SendMsg(string username, string msg)
        {
            if (msg.Trim() == "")
            {
                return this;
            }
            this.InternalSendMessage($"@{username} {Server.NowStr()}: {msg}");
            return this;
        }

        public IServer SendMsgTo(string username, string toUsername, string msg)
        {
            if (msg.Trim() == "")
            {
                return this;
            }
            this.InternalSendMessage($"@{username} {Server.NowStr()}: {msg}", toUsername);
            return this;
        }

        private void InternalSendMessage(string msg, string toUsername = "")
        {
            StreamWriter swSender;
            StatusEventArgs e = new(msg);
            Server.OnStatusChanged(e);

            TcpClient[] clients = new TcpClient[this.users.Count];
            this.users.Values.CopyTo(clients, 0);
            for (int i = 0; i < clients.Length; i++)
            {
                var client = clients[i];
                try
                {
                    if (client == null) { continue; }
                    if (toUsername != "")
                    {
                        string user = $"{this.connections[client]}";
                        if (user != toUsername) { continue; }
                    }

                    swSender = new StreamWriter(client.GetStream());
                    swSender.WriteLine(msg);
                    swSender.Flush();
                    swSender = null;
                }
                catch
                {
                    this.NicknameUnregister(client);
                }
            }
        }
    }
}
