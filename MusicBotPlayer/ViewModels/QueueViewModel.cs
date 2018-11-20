using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicBotPlayer
{
    public class QueueViewModel : BaseViewModel
    {
        private static readonly ObservableCollection<QueueTrack> queue = new ObservableCollection<QueueTrack>();

        public delegate void QueueChangedHandler(object sender, QueueChangedEventArgs e);

        public static event QueueChangedHandler OnQueueChanged;

        public static ObservableCollection<QueueTrack> Queue
        {
            get => queue;
        }

        public static void AddToQueue(QueueTrack track)
        {
            Queue.Add(track);

            QueueChangedEventArgs args = new QueueChangedEventArgs(track);
            OnQueueChanged(null, args);
        }

        public static void RemoveFromQueue(int index)
        {
            queue.RemoveAt(index);
        }
    }
}