using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicBotPlayer
{
    public class QueueTrack : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// The title of the track.
        /// </summary>
        private string title;

        /// <summary>
        /// The Spotify ID of the track, if applicable.
        /// </summary>
        private string spotifyId;

        /// <summary>
        /// The artists of the track.
        /// </summary>
        private string[] artist;

        #endregion

        #region Public Properties

        /// <summary>
        /// The title of the track.
        /// </summary>
        public string Title
        {
            get => title;
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// The Spotify ID of the track, if applicable.
        /// </summary>
        public string SpotifyId
        {
            get => spotifyId;
            set
            {
                spotifyId = value;
            }
        }

        /// <summary>
        /// The artists of the track.
        /// </summary>
        public string[] Artist
        {
            get => artist;
            set
            {
                artist = value;
            }
        }

        #endregion
    }
}