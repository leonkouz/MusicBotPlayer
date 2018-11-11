using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicBotPlayer
{
    public class SpotifyViewModel : BaseViewModel
    {
        private bool isSelected = false;

        /// <summary>
        /// Indicates whether the View Model is selected.
        /// </summary>
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SpotifyViewModel()
        {
        }

        /// <summary>
        /// Fires when a key is pressed while the Search Bar is in focus.
        /// </summary>
        public void Search(string text)
        {

        }
    }
}
