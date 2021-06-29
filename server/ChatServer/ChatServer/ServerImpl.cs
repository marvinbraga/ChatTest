using System;
using System.Collections;

namespace ChatServer
{
    // Define parameters to event.
    public delegate void StatusEventHandler(object sender, StatusEventArgs e);

    class Server: IServer
    {
        private IServerProperties props;
        private Hashtable users;

        public static IServer New(IServerProperties props)
        {
            return new Server(props);
        }

        public Server(IServerProperties props)
        {
            this.props = props;
            this.users = new Hashtable(this.props.MaxUsersNumbers());
        }

        public IServer Run()
        {
            Console.WriteLine(this.props.ToString());
            return this;
        }
    }
}
