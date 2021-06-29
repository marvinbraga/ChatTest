namespace ChatServer
{
    public class StatusEventArgs
    {
        private string events;

        public string EventMessage
        {
            get { return this.events; }
            set { this.events = value; }
        }

        public StatusEventArgs(string events)
        {
            this.events = events;
        }
    }
}
