using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public class QueueTrack : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// The name of the track.
        /// </summary>
        private string name;

        /// <summary>
        /// The duration of the track.
        /// </summary>
        private string duration;

        /// <summary>
        /// The artists of the track.
        /// </summary>
        private string[] artist;

        /// <summary>
        /// The URL of the track's album image.
        /// </summary>
        private string image;

        /// <summary>
        /// The spotify ID of the track, if applicable.
        /// </summary>
        private string spotifyId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the track.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// The duration of the track.
        /// </summary>
        public string Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }

        /// <summary>
        /// The artists of the track.
        /// </summary>
        public string[] Artists
        {
            get => artist;
            set
            {
                artist = value;
                OnPropertyChanged("Artists");
            }
        }

        /// <summary>
        /// The URL of the track's album image.
        /// </summary>
        public string Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        /// <summary>
        /// The spotify id, if applicable.
        /// </summary>
        public string SpotifyId
        {
            get => spotifyId;
            set
            {
                spotifyId = value;
                OnPropertyChanged("SpotifyId");

                if(Image == null)
                {
                    LoadAlbumImage();
                }
            }
        }

        #endregion

        public QueueTrack(string spotifyId)
        {
            this.spotifyId = spotifyId;
        }

        public QueueTrack()
        {

        }

        /// <summary>
        /// Loads the album image.
        /// </summary>
        private void LoadAlbumImage()
        {
            Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Image = Spotify.GetTrackImage(spotifyId);
                });
            });
        }

    }
}