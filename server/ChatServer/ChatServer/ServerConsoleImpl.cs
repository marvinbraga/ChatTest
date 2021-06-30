using System;

namespace ChatServer
{
    class ServerConsole: IServerConsole
    {
        private delegate void UpdateStatusCallback(string msg);
        private IServer server;

        public static IServerConsole New(IServer server)
        {
            return new ServerConsole(server);
        }

        public ServerConsole(IServer server)
        {
            this.server = server;
            this.server.SetStatusEvent(ServerStatusChanged);
        }

        public void Execute()
        {
            this.WaitKey();
        }

        private void WaitKey()
        {
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
