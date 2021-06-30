namespace ChatClient
{
    interface IClient
    {
        public IClient StartConnection();
        public IClient SendMessage(string msg, string toUsername);
        public IClient Run();
    }
}
