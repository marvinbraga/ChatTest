namespace ChatServer
{
    interface IMessage
    {
        public IMessage Send(string text, string username = "", string toUsername = "");
        public IServer Server();
}
}
