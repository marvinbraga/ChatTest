using System;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerConsole.New(
                Server.New(ServerProperties.New(args)).Run()
            ).Execute();
        }
    }
}
