using System;
using System.Net;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isOut = false;
            string username;
            do
            {
                System.Console.WriteLine("Enter your username: ");
                username = System.Console.ReadLine();
                try
                {
                    ClientConsole.New(Client.New(username, IPAddress.Parse("127.0.0.1"), 8081).Run()).Execute();
                }
                catch (LogoutException)
                {
                    isOut = false;
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
    }
}
