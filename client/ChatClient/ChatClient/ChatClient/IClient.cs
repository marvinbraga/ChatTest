namespace ChatClient
{
    public interface IClient
    {
        public IClient StartConnection();
        public IClient SendMessage(string msg, string toUsername);
        public IClient Run();
        public IClient SetStatusEvent(StatusEventHandler statusEvent);
        public string Username();
    }
}
