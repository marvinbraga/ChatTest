using System;
using System.Net;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client.New("marvin", IPAddress.Parse("127.0.0.1"), 8081).Run();
            System.Console.WriteLine("Press ESC to exit...");
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
            } while (keyinfo.Key != ConsoleKey.Escape);
        }
    }
}
