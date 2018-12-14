using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicBotPlayer
{
    public class QueueViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// The full duration of the currently playing track.
        /// </summary>
        private TimeSpan trackDuration;

        /// <summary>
        /// The current time of the song being played.
        /// </summary>
        private TimeSpan currentPointInTrack;

        /// <summary>
        /// The parent View Model.
        /// </summary>
        private ApplicationViewModel parentViewModel;

        /// <summary>
        /// The list of tracks currently in the queue.
        /// </summary>
        private readonly ObservableCollection<QueueTrack> queue = new ObservableCollection<QueueTrack>();

        /// <summary>
        /// The history of tracks played. 
        /// </summary>
        private ObservableCollection<QueueTrack> history = new ObservableCollection<QueueTrack>();

        /// <summary>
        /// Specifies whether the queue is currently playing or not.
        /// </summary>
        private bool isPlaying = false;

        /// <summary>
        /// The track that is currently playing.
        /// </summary>
        private QueueTrack currentlyPlayingTrack;

        

        #endregion

        #region Public Properties

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
        /// The current time of the song being played.
        /// </summary>
        public TimeSpan CurrentPointInTrack
        {
            get => currentPointInTrack;
            set
            {
                currentPointInTrack = value;
                OnPropertyChanged("CurrentPointInTrack");
            }
        }

        /// <summary>
        /// The full duration of the currently playing track.
        /// </summary>
        public TimeSpan TrackDuration
        {
            get => trackDuration;
            set
            {
                trackDuration = value;
                OnPropertyChanged("TrackDuration");
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// QueueChanged event delegate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void QueueChangedHandler(object sender, QueueChangedEventArgs e);

        /// <summary>
        /// QueueChanged event.
        /// </summary>
        public event QueueChangedHandler OnQueueChanged;

        #endregion

        #region Commands

        /// <summary>
        /// The command to skip to the next track.
        /// </summary>
        public ICommand NextTrackCommand { get; set; }

        /// <summary>
        /// The command to go to the previous track.
        /// </summary>
        public ICommand PreviousTrackCommand { get; set; }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public QueueViewModel(ApplicationViewModel parentViewModel)
        {
            this.parentViewModel = parentViewModel;

            this.OnQueueChanged += QueueViewModel_OnQueueChanged;
            DiscordBot.TrackFinishedPlaying += DiscordBot_TrackFinishedPlaying;

            NextTrackCommand = new DelegateCommand(OnNextTrack);
            PreviousTrackCommand = new DelegateCommand(OnPreviousTrack);
        }


        private void DiscordBot_TrackFinishedPlaying(object sender, EventArgs e)
        {
            IsPlaying = false;
        }

        /// <summary>
        /// Skips to the next track in the queue.
        /// </summary>
        private async void OnNextTrack()
        {
            await Task.Run(() =>
            {
                if(DiscordBot.IsTransitioningToNextTrack == true)
                {
                    return;
                }

                AddCurrentTrackToHistory();

                SendStopPlayingCommandToDiscordBot();

                AddNextTrackFromQueueToCurrentTrack();
            });
        }
        
        /// <summary>
        /// Goes to the previous track in the history.
        /// </summary>
        private void OnPreviousTrack()
        {
            if (DiscordBot.IsTransitioningToNextTrack == true)
            {
                return;
            }

            if (history.Count != 0)
            {
                if (IsCurrentTrackAlreadyNextInQueue() == false)
                {
                    AddCurrentTrackToQueue();

                    SendStopPlayingCommandToDiscordBot();

                    RetrievePreviousTrackFromHistory();
                }
                else
                {
                    RetrievePreviousTrackFromHistory();
                }
            }
        }

        /// <summary>
        /// Sets <see cref="CurrentlyPlayingTrack"/> to the next track 
        /// from the queue and removes the first track from the Queue.
        /// </summary>
        private void AddNextTrackFromQueueToCurrentTrack()
        {
            if (Queue.Count != 0)
            {
                CurrentlyPlayingTrack = Queue.First();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Queue.Remove(Queue.First());
                });

                RaiseQueueEventChanged(CurrentlyPlayingTrack);
            }
            else
            {
                CurrentlyPlayingTrack = null;
            }
        }

        /// <summary>
        /// Sets the CurrentlyPlayingTrack as the previous track played.
        /// </summary>
        private void RetrievePreviousTrackFromHistory()
        {
            if (history.Count != 0)
            {
                // Get the last track from history as that 
                // would be the most recent track added to history.
                CurrentlyPlayingTrack = history.Last();

                history.Remove(history.Last());

                RaiseQueueEventChanged(CurrentlyPlayingTrack);
            }
            else
            {
                // Rewind to the start of the song.
            }
        }

        /// <summary>
        /// Determines if the current track is already the next track
        /// in queue.
        /// </summary>
        /// <returns>True if next track in queue is the same, false if not.</returns>
        private bool IsCurrentTrackAlreadyNextInQueue()
        {
            // If current track is not null
            if(CurrentlyPlayingTrack != null)
            {
                // If the queue does not contain any tracks.
                if(Queue.Count != 0)
                {
                    if (CurrentlyPlayingTrack.Name == Queue.First().Name &&
                         CurrentlyPlayingTrack.Artists[0] == Queue.First().Artists[0])
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the currently playing track to history.
        /// </summary>
        private void AddCurrentTrackToHistory()
        {
            if(CurrentlyPlayingTrack != null)
            {
                history.Add(CurrentlyPlayingTrack);
            }
        }

        /// <summary>
        /// Add the current track to the queue.
        /// </summary>
        private void AddCurrentTrackToQueue()
        {
            if(CurrentlyPlayingTrack != null)
            {
                // Insert the track to the start of the queue
                // so the next track will be the current track.
                Queue.Insert(0, CurrentlyPlayingTrack);
            }
        }

        /// <summary>
        /// Fires when the queue is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            // If the bot is not playing, play the current track.
            if (IsPlaying == false)
            {
                PlayCurrentTrack();
            }
        }

        /// <summary>
        /// Play the current track.
        /// </summary>
        public void PlayCurrentTrack()
        {
            IsPlaying = true;

            // Don't play next track if the bot is currently transitioning to the next song.
            if(DiscordBot.IsTransitioningToNextTrack == true)
            {
                return;
            }

            Task.Run(async () =>
            {
                try
                {
                    await DiscordBot.Play(CurrentlyPlayingTrack);
                }
                catch (DiscordBotNotConnectedException)
                {
                    IsPlaying = false;
                    CurrentlyPlayingTrack = null;
                    currentPointInTrack = TimeSpan.Zero;
                    TrackDuration = TimeSpan.Zero;
                    history.Clear();
                    queue.Clear();

                    MessageBox.Show("Must join a voice channel before playing a song.");

                    return;
                }
            });
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

        /// <summary>
        /// Send the command to the Discord Bot to stop playing music.
        /// </summary>
        private async void SendStopPlayingCommandToDiscordBot()
        {
            await DiscordBot.StopPlaying();
            IsPlaying = false;
        }

    }
}