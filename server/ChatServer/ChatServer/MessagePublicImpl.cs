namespace ChatServer
{
    sealed class PublicMessage : Message
    {
        public static IMessage New(IServer server)
        {
            return new PublicMessage(server);
        }

        public PublicMessage(IServer server) : base(server) { }

        protected override string GetAdjustedMessage(string text, string username, string toUsername)
        {
            return $"@{username} {Message.NowStr()}: {text}";
        }
    }
}
