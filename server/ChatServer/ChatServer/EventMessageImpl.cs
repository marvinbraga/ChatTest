namespace ChatServer
{
    class EventMessage : IEventMessage
    {
        private string text;

        public static IEventMessage New(string text)
        {
            return new EventMessage(text);
        }

        public EventMessage(string text)
        {
            this.text = text;
        }

        public string Text()
        {
            return this.text;
        }
    }
}
