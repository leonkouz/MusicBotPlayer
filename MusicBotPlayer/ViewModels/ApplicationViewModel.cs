using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicBotPlayer
{
    public class ApplicationViewModel : BaseViewModel
    {
        /// <summary>
        /// The Youtube View Model.
        /// </summary>
        public YoutubeViewModel YoutubeViewModel { get; set; } = new YoutubeViewModel();

        /// <summary>
        /// The Spotify View Model.
        /// </summary>
        public SpotifyViewModel SpotifyViewModel { get; set; } = new SpotifyViewModel();

        /// <summary>
        /// The Queue View Model.
        /// </summary>
        public QueueViewModel QueueViewModel { get; set; } 

        public ApplicationViewModel()
        {
            QueueViewModel = new QueueViewModel(this);

            SpotifyViewModel.IsSelected = true;

            SideMenuButtonClickedCommand = new DelegateCommand(OnSideMenuButtonClicked);

            //Thread discordThread = new Thread(new ThreadStart(DiscordBot.InitialiseBot));
            //discordThread.Start();
        }
       
        /// <summary>
        /// The command for when a <see cref="SideMenuButton"/> is clicked.
        /// </summary>
        public ICommand SideMenuButtonClickedCommand { get; set; }

        /// <summary>
        /// Fires when a <see cref="SideMenuButton"/> is clicked.
        /// </summary>
        private void OnSideMenuButtonClicked()
        {

        }
    }
}
