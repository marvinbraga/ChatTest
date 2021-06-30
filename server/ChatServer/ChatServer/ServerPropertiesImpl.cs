using System;
using System.Net;

namespace ChatServer
{
    class ServerProperties : IServerProperties
    {
        private IPAddress host;
        private int port;
        private int maxUsersNumber;
        private int maxConnectionsNumber;

        protected void Validate(string[] args)
        {
            if (args.Length > 3)
            {
                throw new ArgumentException("Informe apenas o Host e a Porta para inicializar o servidor.");
            }
        }

        protected static (IPAddress argHost, int argPort, int argMaxUsersNumber, int argMaxConnectionsNumber) GetArgs(string[] args)
        {
            // Default properties values.
            IPAddress argHost = IPAddress.Parse("127.0.0.1");
            int argPort = 8081;
            int argMaxUsersNumber = 10;
            int argMaxConnectionsNumber = 50;

            if (args.Length > 0)
            {
                argHost = args[0] != null ? IPAddress.Parse(args[0]) : argHost;
                if (args.Length > 1)
                {
                    argPort = args[1] != null ? Int32.Parse(args[1]) : argPort;
                    if (args.Length > 2)
                    {
                        argMaxUsersNumber = args[2] != null ? Int32.Parse(args[2]) : argMaxUsersNumber;
                        if (args.Length > 3)
                        {
                            argMaxConnectionsNumber = args[3] != null ? Int32.Parse(args[3]) : argMaxConnectionsNumber;
                        }
                    }
                }
            }
            return (argHost, argPort, argMaxUsersNumber, argMaxConnectionsNumber);
        }

        public static IServerProperties New(string[] args)
        {
            return new ServerProperties(args);
        }

        public ServerProperties(string[] args)
        {
            this.Validate(args);
            var (argHost, argPort, argMaxUsersNumber, argMaxConnectionsNumber) = ServerProperties.GetArgs(args);

            this.host = argHost;
            this.port = argPort;
            this.maxUsersNumber = argMaxUsersNumber;
            this.maxConnectionsNumber = argMaxConnectionsNumber;
        }

        public IPAddress Host()
        {
            return this.host;
        }

        public int Port()
        {
            return this.port;
        }

        public int MaxUsersNumber()
        {
            return this.maxUsersNumber;
        }

        public int MaxConnectionsNumber()
        {
            return this.maxConnectionsNumber;
        }

        public override string ToString()
        {
            return String.Format($"Server(host: {this.host}, port: {this.port}, maxUsersNuber: {this.maxUsersNumber})");
        }
    }
}
