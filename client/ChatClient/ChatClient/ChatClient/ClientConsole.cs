using System;

namespace ChatClient
{
    class ClientConsole : IClientConsole
    {
        private delegate void UpdateStatusCallback(string msg);
        private IClient client;
        private IClientFactory clientFactory;

        public static IClientConsole New(IClientFactory clientFactory) => new ClientConsole(clientFactory);

        public ClientConsole(IClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public void Execute()
        {
            bool isOut = false;
            string username;
            do
            {
                System.Console.WriteLine("Enter your username: ");
                username = System.Console.ReadLine();
                try
                {
                    if (this.client != null)
                    {
                        this.client.CloseConnection();
                        this.client = null;
                    }
                    this.client = this.clientFactory.Make(username);
                    this.client.SetStatusEvent(ClientStatusChanged);
                    this.GetCommand();
                }
                catch (LogoutException)
                {
                    isOut = true;
                    System.Console.WriteLine("Disconnected. Bye!");
                }
                catch (ClientException)
                {
                    isOut = true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    isOut = true;
                }
            } while (!isOut);
        }

        private string GetConsoleCommand(string username)
        {
            System.Console.WriteLine("[Commands: /exit, /priv, /pub, /logout] ");
            System.Console.Write($"{username} enter the command: ");
            return System.Console.ReadLine();
        }

        private void GetCommand()
        {
            string command;
            string username = this.client.Username();
            do
            {
                command = this.GetConsoleCommand(username);
                string msg = "";
                string toUsername = "";

                if (command == "/logout")
                {
                    throw new LogoutException();
                }
                else if (command != "/exit")
                {
                    System.Console.Write($"{username} enter the message: ");
                    msg = System.Console.ReadLine();

                    if (command == "/priv")
                    {
                        System.Console.Write($"Inform the username who will receive the private message: ");
                        toUsername = System.Console.ReadLine();
                        msg = $"900||{username}||{toUsername}||{msg}";
                    }
                    else
                    {
                        msg = $"700||{username}||null||{msg}";
                    }
                    this.client.SendMessage(msg, toUsername);
                }
                else
                {
                    throw new ClientException("Application exit.");
                }
            } while (command != "/exit");
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
