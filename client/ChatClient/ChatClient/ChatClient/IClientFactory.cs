namespace ChatClient
{
    public interface IClientFactory
    {
        public IClient Make(string username);
    }
}
