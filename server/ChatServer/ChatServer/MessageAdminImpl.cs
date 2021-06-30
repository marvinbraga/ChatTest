namespace ChatServer
{
    sealed class AdminMessage : Message
    {
        public static IMessage New(IServer server)
        {
            return new AdminMessage(server);
        }

        public AdminMessage(IServer server) : base(server) { }

        protected override string GetAdjustedMessage(string text, string username, string toUsername)
        {
            return $"@admin {Message.NowStr()}: {text}";
        }
    }
}
