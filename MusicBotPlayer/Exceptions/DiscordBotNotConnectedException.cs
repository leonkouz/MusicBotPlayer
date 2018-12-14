using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public class DiscordBotNotConnectedException : Exception
    {
        public DiscordBotNotConnectedException()
        {
        }

        public DiscordBotNotConnectedException(string message)
        : base(message)
        {
        }

        public DiscordBotNotConnectedException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
