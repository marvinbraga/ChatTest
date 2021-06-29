using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    interface IServerProperties
    {
        public string Host();
        public int Port();

        public int MaxUsersNumbers();
    }
}
