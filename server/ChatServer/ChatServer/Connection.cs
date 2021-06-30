using System.Net.Sockets;

namespace ChatServer
{
    interface IConnection
    {
        public TcpClient Client();
    }
}
