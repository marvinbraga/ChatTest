using System.Net;

namespace ChatClient
{
    public class ClientFactory : IClientFactory
    {
        private IPAddress host;
        private int port;

        public static IClientFactory New(IPAddress host, int port) => new ClientFactory(host, port);

        public ClientFactory(IPAddress host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public IClient Make(string username) => Client.New(this.host, this.port, username).Run();
    }
}
