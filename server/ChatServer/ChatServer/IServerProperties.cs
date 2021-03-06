using System.Net;

namespace ChatServer
{
    interface IServerProperties
    {
        public IPAddress Host();
        public int Port();
        public int MaxUsersNumber();
        public int MaxConnectionsNumber();
    }
}
