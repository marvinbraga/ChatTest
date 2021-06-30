using System.Collections;
using System.Net.Sockets;

namespace ChatServer
{
    interface IServer
    {
        public IServer Run();
        public bool NicknameRegister(TcpClient tcpConn, string username);
        public IServer NicknameUnregister(TcpClient tcpConn);
        public IServer SetStatusEvent(StatusEventHandler statusEvent);
        public Hashtable Users();
        public Hashtable Connections();
    }
}
