namespace ChatServer
{
    sealed class PrivateMessage : Message
    {
        public static IMessage New(IServer server)
        {
            return new PrivateMessage(server);
        }

        public PrivateMessage(IServer server) : base(server) { }

        protected override string GetAdjustedMessage(string text, string username, string toUsername)
        {
            return $"@{username} to @{toUsername} {Message.NowStr()}: {text}";
        }
    }
}
