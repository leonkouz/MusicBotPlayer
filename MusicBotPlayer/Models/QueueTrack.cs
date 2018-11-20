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