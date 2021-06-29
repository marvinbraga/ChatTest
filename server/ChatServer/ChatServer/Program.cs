using System;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.New(ServerProperties.New(args)).Run();
        }
    }
}
