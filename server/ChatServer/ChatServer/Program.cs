using System;

namespace ChatServer
{
    class Program
    {
        private delegate void UpdateStatusCallback(string msg);

        static void Main(string[] args)
        {
            RunServer(Server.New(ServerProperties.New(args), ServerStatusChanged));
        }

        private static void RunServer(IServer server)
        {
            server.Run();
            ShowStatus("Press ESC to exit...");
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
            } while (keyinfo.Key != ConsoleKey.Escape);
        }

        public static void ServerStatusChanged(object sender, StatusEventArgs e)
        {
            UpdateStatusCallback updater = new UpdateStatusCallback(ShowStatus);
            if (updater != null)
            {
                updater(e.EventMessage);
            }
        }

        private static void ShowStatus(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }
}
