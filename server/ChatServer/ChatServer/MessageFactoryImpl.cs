using System;

namespace ChatServer
{
    class MessageFactory
    {
        public static void Send(IServer server, string msg, string username)
        {
            // Public Message  = 700||username||null||Texto da mensagem
            // Private Message = 900||username||toUsername||Texto da mensagem
            string[] attrs = msg.Split("||");
            int cmd = Int32.Parse(attrs[0]);
            switch (cmd)
            {
                case 700:
                    PublicMessage.New(server).Send(attrs[3], username);
                    break;
                case 900:
                    PrivateMessage.New(server).Send(attrs[3], username, attrs[2]);
                    break;
            }
        }
    }
}
