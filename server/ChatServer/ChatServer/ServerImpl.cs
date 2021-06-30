using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ChatServer
{
    public delegate void StatusEventHandler(object sender, StatusEventArgs e);

    class Server: IServer
    {
        private IServerProperties props;
        private Hashtable users;
        private Hashtable connections;
        private TcpClient client;
        private Thread listener;
        private TcpListener server;
        private bool isRunning = false;
        public static event StatusEventHandler StatusChanged;

        public static IServer New(IServerProperties props)
        {
            return new Server(props);
        }

        public Server(IServerProperties props)
        {
            this.props = props;
            this.users = new Hashtable(this.props.MaxUsersNumber());
            this.connections = new Hashtable(this.props.MaxConnectionsNumber());
        }

        public IServer SetStatusEvent(StatusEventHandler statusEvent)
        {
            Server.StatusChanged += statusEvent;
            return this;
        }

        public IServer Run()
        {
            try
            {
                this.server = new TcpListener(this.props.Host(), this.props.Port());
                this.server.Start();
                this.isRunning = true;
                this.listener = new Thread(this.AddListener);
                this.listener.IsBackground = true;
                this.listener.Start();
                System.Console.WriteLine(this.props.ToString());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
            }
            return this;
        }

        private void AddListener()
        {
            while (this.isRunning)
            {
                this.client = this.server.AcceptTcpClient();
                Connection conn = new(this.client, this);
            }
        }

        protected static string NowStr()
        {
            return $"{DateTime.Now:dd\\/MM\\/yyyy h\\:mm tt}".Trim();
        }

        public bool NicknameRegister(TcpClient conn, string username)
        {
            bool notContains = !this.users.Contains(username);
            if (notContains)
            {
                this.users.Add(username, conn);
                this.connections.Add(conn, username);
                this.SendMsgAdmin($"[USER REGISTER] The user {username} has joined us.");
            }
            return notContains;
        }

        public IServer NicknameUnregister(TcpClient conn)
        {
            if (this.connections[conn] != null)
            {
                string user = $"{this.connections[conn]}";
                this.users.Remove(this.connections[conn]);
                this.connections.Remove(conn);
                this.SendMsgAdmin($"[USER UNREGISTER] The user {user} has logged out.");
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
