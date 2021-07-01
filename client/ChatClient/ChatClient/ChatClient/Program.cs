using System;
using System.Net;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientConsole.New(ClientFactory.New(IPAddress.Parse("127.0.0.1"), 8081)).Execute();
        }
    }
}