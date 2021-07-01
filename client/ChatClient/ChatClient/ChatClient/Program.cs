using System;
using System.Net;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            ClientConsole.New(Client.New(username, IPAddress.Parse("127.0.0.1"), 8081).Run()).Execute();
        }
    }
}
