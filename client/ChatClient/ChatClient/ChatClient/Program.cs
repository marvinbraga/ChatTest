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
            IClient client = Client.New(username, IPAddress.Parse("127.0.0.1"), 8081).Run();
            string command;
            do
            {
                Console.WriteLine("[Commands: /exit, /priv, /pub] ");
                Console.Write($"{username} enter the command: ");
                command = Console.ReadLine();
                string msg = "";
                string toUsername = "";
                if (command != "/exit")
                {
                    Console.Write($"{username} enter the message: ");
                    msg = Console.ReadLine();
                    if (command == "/priv")
                    {
                        Console.Write($"Inform the username who will receive the private message: ");
                        toUsername = Console.ReadLine();
                        msg = $"900||{username}||{toUsername}||{msg}";
                    }
                    else 
                    {
                        msg = $"700||{username}||null||{msg}";
                    }
                    client.SendMessage(msg, toUsername);
                }
            } while (command != "/exit");
        }


    }
}
