using System.Net.Sockets;

namespace ChatServer
{
    interface IServer
    {
        public IServer Run();
        public bool NicknameRegister(TcpClient tcpConn, string username);
        public IServer NicknameUnregister(TcpClient tcpConn);
        public IServer SendMsgAdmin(string msg);
        public IServer SendMsg(string username, string msg);
        public IServer SendMsgTo(string username, string toUsername, string msg);
        public IServer SetStatusEvent(StatusEventHandler statusEvent);
    }
}
