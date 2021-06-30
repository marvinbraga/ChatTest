using System;
using System.Net;

namespace ChatServer
{
    class ServerProperties : IServerProperties
    {
        private IPAddress host;
        private int port;
        private int maxUsersNumber;

        protected void Validate(string[] args)
        {
            if (args.Length > 3)
            {
                throw new ArgumentException("Informe apenas o Host e a Porta para inicializar o servidor.");
            }
        }

        protected static (IPAddress argHost, int argPort, int argMaxUsersNumber) GetArgs(string[] args)
        {
            // Default properties values.
            IPAddress argHost = IPAddress.Parse("127.0.0.1");
            int argPort = 8081;
            int argMaxUsersNumber = 10;
            if (args.Length > 0)
            {
                argHost = args[0] != null ? IPAddress.Parse(args[0]) : argHost;
                if (args.Length > 1)
                {
                    argPort = args[1] != null ? Int32.Parse(args[1]) : argPort;
                    if (args.Length > 2)
                    {
                        argMaxUsersNumber = args[2] != null ? Int32.Parse(args[2]) : argMaxUsersNumber;
                    }
                }
            }
            return (argHost, argPort, argMaxUsersNumber);
        }

        public static IServerProperties New(string[] args)
        {
            return new ServerProperties(args);
        }

        public ServerProperties(string[] args)
        {
            this.Validate(args);
            var (argHost, argPort, argMaxUsersNumber) = ServerProperties.GetArgs(args);

            this.host = argHost;
            this.port = argPort;
            this.maxUsersNumber = argMaxUsersNumber;
        }

        public IPAddress Host()
        {
            return this.host;
        }

        public int Port()
        {
            return this.port;
        }

        public int MaxUsersNumbers()
        {
            return this.maxUsersNumber;
        }

        public override string ToString()
        {
            return String.Format($"Server(host: {this.host}, port: {this.port}, maxUsersNuber: {this.maxUsersNumber})");
        }
    }
}
