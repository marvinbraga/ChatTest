using System;

namespace ChatClient
{
    class LogoutException : Exception
    {
        public LogoutException() : base() { }
        public LogoutException(string message) : base(message) { }
        public LogoutException(string message, Exception inner) : base(message, inner) { }
        protected LogoutException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
