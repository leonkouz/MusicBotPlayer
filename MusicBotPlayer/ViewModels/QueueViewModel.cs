using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public class QueueViewModel : BaseViewModel
    {
        private ApplicationViewModel parentViewModel;

        /// <summary>
        /// The list of tracks currently in the queue.
        /// </summary>
        private readonly ObservableCollection<QueueTrack> queue = new ObservableCollection<QueueTrack>();

        /// <summary>
        /// Specifies whether the queue is currently playing or not.
        /// </summary>
        private bool isPlaying = false;

        /// <summary>
        /// The track that is currently playing.
        /// </summary>
        private QueueTrack currentlyPlayingTrack;

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
                OnPropertyChanged("IsPlaying");
            }
        }

        /// <summary>
        /// The track that is currently playing.
        /// </summary>
        public QueueTrack CurrentlyPlayingTrack
        {
            get => currentlyPlayingTrack;
            set
            {
                currentlyPlayingTrack = value;
                OnPropertyChanged("CurrentlyPlayingTrack");
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
        /// Constructor.
        /// </summary>
        public QueueViewModel(ApplicationViewModel parentViewModel)
        {
            this.parentViewModel = parentViewModel;

            this.OnQueueChanged += QueueViewModel_OnQueueChanged;
        }

        private void QueueViewModel_OnQueueChanged(object sender, QueueChangedEventArgs e)
        {
            // If there is no currently playing track.
            if (CurrentlyPlayingTrack == null)
            {
                // Add the first track in the queue to the currently playing track.
                CurrentlyPlayingTrack = Queue.First();
                // Remove the track from the queue.
                Queue.Remove(e.Track);
            }
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
            QueueTrack track = null;

            App.Current.Dispatcher.Invoke(() =>
            {
                track = new QueueTrack()
                {
                    Artists = StringHelper.StringToArray(trackSearch.TrackArtists),
                    Name = trackSearch.TrackName,
                    Duration = trackSearch.TrackLength,
                    SpotifyId = trackSearch.TrackId                    
                };

            });

            return track;
        }

        /// <summary>
        /// Raise the QueueChanged event.
        /// </summary>
        /// <param name="track">The track to parse.</param>
        private void RaiseQueueEventChanged(QueueTrack track)
        {
            if (OnQueueChanged != null)
            {
                QueueChangedEventArgs args = new QueueChangedEventArgs(track);
                OnQueueChanged(null, args);
            }
        }
    }
}