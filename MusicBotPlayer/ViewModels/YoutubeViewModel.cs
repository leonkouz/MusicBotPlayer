using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public class YoutubeViewModel : BaseViewModel
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


    }
}
