using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatClient
{
    class Client: IClient
    {
        private string username;
        private StreamWriter sender;
        private StreamReader reader;
        private TcpClient server;
        private delegate void UpdateLogCallBack(string msg);
        private delegate void CloseConnectionCallBack(string info);
        private Thread thread;
        private IPAddress host;
        private int port;
        private bool isConnected;

        public static IClient New(string username, IPAddress host, int port)
        {
            return new Client(username, host, port);
        }

        public Client(string username, IPAddress host, int port)
        {
            this.username = username;
            this.host = host;
            this.port = port;
        }

        public IClient Run()
        {
            if (!this.isConnected)
            {
                this.StartConnection();
            }
            return this;
        }

        public IClient StartConnection()
        {
            try
            {
                this.server = new TcpClient();
                this.server.Connect(this.host, this.port);
                this.isConnected = true;
                this.sender = new StreamWriter(this.server.GetStream());
                this.sender.WriteLine(this.username);
                this.sender.Flush();
                this.thread = new Thread(new ThreadStart(this.ReadMessages));
                this.thread.IsBackground = true;
                this.thread.Start();
                this.UpdateLog($"Connected to Server: (username: {this.username}, ip: {this.host}, port: {this.port}.)");
            }
            catch (Exception ex)
            {
                this.CloseConnection($"Server connection error: {ex.Message}");
            }
            return this;
        }

        public void ReadMessages()
        {
            this.reader = new StreamReader(this.server.GetStream());
            string response = this.reader.ReadLine();
            if (response[0] == '@')
            {
                this.UpdateLog("Success connection.");
            }
            else
            {
                CloseConnectionCallBack closeConn = new(this.CloseConnection);
                if (closeConn != null)
                {
                    closeConn($"Connect failed: {response.Substring(2, response.Length - 2)}.");
                }
                return;
            }
            while (this.isConnected)
            {
                UpdateLogCallBack updater = new(this.UpdateLog);
                if (updater != null)
                {
                    updater(this.reader.ReadLine());
                }
            }
        }

        public IClient SendMessage(string msg, string toUsername = "")
        {
            if (msg.Trim() != "")
            {
                this.sender.WriteLine(msg);
                this.sender.Flush();
            }
            return this;
        }

        public void CloseConnection(string info)
        {
            this.UpdateLog(info);
            this.sender.Close();
            this.reader.Close();
            this.server.Close();
            this.isConnected = false;
        }

        private void UpdateLog(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }
}
