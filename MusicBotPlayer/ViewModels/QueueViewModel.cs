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
        /// <summary>
        /// The list of tracks currently in the queue.
        /// </summary>
        private readonly ObservableCollection<QueueTrack> queue = new ObservableCollection<QueueTrack>();

        /// <summary>
        /// Specifies whether the queue is currently playing or not.
        /// </summary>
        private bool isPlaying = false;

        public delegate void QueueChangedHandler(object sender, QueueChangedEventArgs e);

        public event QueueChangedHandler OnQueueChanged;

        /// <summary>
        /// Specifies whether the queue is currently playing or not.
        /// </summary>
        public bool IsPlaying
        {
            get => isPlaying;
            set
            {
                isPlaying = value;
            }
        }

        /// <summary>
        /// The list of tracks currently in the queue.
        /// </summary>
        public ObservableCollection<QueueTrack> Queue
        {
            get => queue;
        }

        /// <summary>
        /// Adds the specified <see cref="QueueTrack"/> to the queue.
        /// </summary>
        /// <param name="track">The track</param>
        public void AddToQueue(QueueTrack track)
        {
            Queue.Add(track);

            RaiseQueueEventChanged(track);
        }

        /// <summary>
        /// Adds the specified <see cref="TrackSearchItem"/> to the queue.
        /// </summary>
        /// <param name="track"></param>
        public void AddToQueue(TrackSearchItem track)
        {
            QueueTrack qTrack = ConvertToQueueTrack(track);

            Queue.Add(qTrack);

            RaiseQueueEventChanged(qTrack);
        }

        /// <summary>
        /// Removes a track from the queue at the specified index.
        /// </summary>
        /// <param name="index">The index to remove the track at.</param>
        public void RemoveFromQueue(int index)
        {
            queue.RemoveAt(index);
        }

        /// <summary>
        /// Converts a <see cref="TrackSearchItem"/> object to a <see cref="QueueTrack"/> object.
        /// </summary>
        /// <param name="trackSearch"></param>
        /// <returns></returns>
        private QueueTrack ConvertToQueueTrack(TrackSearchItem trackSearch)
        {
            QueueTrack track = new QueueTrack
            {
                Artist = StringHelper.StringToArray(trackSearch.TrackArtists),
                Name = trackSearch.TrackName,
                Duration = trackSearch.TrackLength
            };

            return track;
        }

        /// <summary>
        /// Raise the QueueChanged event.
        /// </summary>
        /// <param name="track">The track to parse.</param>
        private void RaiseQueueEventChanged(QueueTrack track)
        {
            if(OnQueueChanged == null)
            {
                return;
            }

            QueueChangedEventArgs args = new QueueChangedEventArgs(track);
            OnQueueChanged(null, args);
        }
    }
}