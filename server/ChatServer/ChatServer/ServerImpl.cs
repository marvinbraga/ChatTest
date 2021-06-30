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

        public static IServer New(IServerProperties props) => new Server(props);

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
                IConnection conn = Connection.New(this.client, this);
            }
        }

        public bool NicknameRegister(TcpClient conn, string username)
        {
            // Issue #1 - Register.
            bool notContains = !this.users.Contains(username);
            if (notContains)
            {
                this.users.Add(username, conn);
                this.connections.Add(conn, username);
                AdminMessage.New(this).Send($"[USER REGISTER] The user {username} has joined us.");
            }
            return notContains;
        }

        public IServer NicknameUnregister(TcpClient conn)
        {
            // Issue #4 - Logout.
            if (this.connections[conn] != null)
            {
                string user = $"{this.connections[conn]}";
                this.users.Remove(this.connections[conn]);
                this.connections.Remove(conn);
                AdminMessage.New(this).Send($"[USER UNREGISTER] The user {user} has logged out.");
            }
            return this;
        }

        public Hashtable Users()
        {
            return this.users;
        }

        public Hashtable Connections()
        {
            return this.connections;
        }

        public static void OnStatusChanged(StatusEventArgs e)
        {
            StatusEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                statusHandler(null, e);
            }
        }
    }
}
