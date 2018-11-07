using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicBotPlayer
{
    public class ApplicationViewModel : BaseViewModel
    {
        private ObservableCollection<BaseViewModel> integrations = new ObservableCollection<BaseViewModel>();

        public ObservableCollection<BaseViewModel> Integrations
        {
            get => Integrations;
        }

        public ApplicationViewModel()
        {
            // Initialise integration view models
            YoutubeViewModel youtubeViewModel = new YoutubeViewModel();
            SpotifyViewModel spotifyViewModel = new SpotifyViewModel();

            // Add integrations to list.
            integrations.Add(youtubeViewModel);
            integrations.Add(spotifyViewModel);

            MenuButtonClickedCommand = new DelegateCommand(OnSendMessage);
        }
       
        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand MenuButtonClickedCommand { get; set; }

        private void OnSendMessage()
        {


        }


    }
}
