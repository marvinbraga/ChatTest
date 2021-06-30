using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ChatServer
{
    class Connection: IConnection
    {
        private TcpClient client;
        private Thread th;
        private StreamReader reader;
        private StreamWriter sender;
        private string username;
        private string response;
        private IServer server;

        public Connection(TcpClient client, IServer server)
        {
            this.client = client;
            this.server = server;
            this.th = new Thread(this.AcceptClient);
            this.th.IsBackground = true;
            this.th.Start();
        }

        private void AcceptClient()
        {
            this.reader = new StreamReader(this.client.GetStream());
            this.sender = new StreamWriter(this.client.GetStream());
            this.username = this.reader.ReadLine();
            if (this.username != "")
            {
                if (this.username == "admin")
                {
                    this.sender.WriteLine($"0| The user {this.username} is locked.");
                    this.sender.Flush();
                    this.Close();
                    return;
                }
                else
                {
                    if (this.server.NicknameRegister(this.client, this.username))
                    {
                        this.sender.WriteLine("1");
                        this.sender.Flush();
                    }
                    else
                    {
                        this.sender.WriteLine($"0| The user {this.username} is logged.");
                        this.sender.Flush();
                        this.Close();
                        return;
                    }
                }
                this.sender.Flush();
            }
            else
            {
                this.Close();
                return;
            }

            try
            {
                while ((this.response = this.reader.ReadLine().Trim()) != "")
                {
                    if (this.response == null)
                    {
                        this.server.NicknameUnregister(this.client);
                    }
                    else
                    {
                        // TODO: Aqui é para recuperar os comandos.
                        this.server.SendMsg(this.username, this.response);
                    }
                }
            }
            catch
            {
                this.server.NicknameUnregister(this.client);
            }
        }

        private void Close()
        {
            this.client.Close();
            this.reader.Close();
            this.sender.Close();
        }

        public TcpClient Client() 
        {
            return this.client;
        }

        public string Username()
        {
            return this.username;
        }
    }
}
