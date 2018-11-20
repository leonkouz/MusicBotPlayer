using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicBotPlayer
{
    public class QueueChangedEventArgs : EventArgs
    {
        public QueueTrack Track { get; private set; }

        public QueueChangedEventArgs(QueueTrack track)
        {
            Track = track;
        }
    }
}