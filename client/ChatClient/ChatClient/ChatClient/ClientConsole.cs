using System;

namespace ChatClient
{
    class ClientConsole : IClientConsole
    {
        private delegate void UpdateStatusCallback(string msg);
        private IClient client;

        public static IClientConsole New(IClient client)
        {
            return new ClientConsole(client);
        }

        public ClientConsole(IClient client)
        {
            this.client = client;
            this.client.SetStatusEvent(ClientStatusChanged);
        }

        public void Execute()
        {
            string command;
            string username = this.client.Username();
            do
            {
                Console.WriteLine("[Commands: /exit, /priv, /pub, /logout] ");
                Console.Write($"{username} enter the command: ");
                command = Console.ReadLine();
                string msg = "";
                string toUsername = "";
                if (command == "/logout")
                {
                    throw new LogoutException();
                }
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
            if (command == "/exit")
            {
                throw new ClientException("Application exit.");
            }
        }

        public static void ClientStatusChanged(object sender, string e)
        {
            UpdateStatusCallback updater = new UpdateStatusCallback(ShowStatus);
            if (updater != null)
            {
                updater(e);
            }
        }

        private static void ShowStatus(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }
}
