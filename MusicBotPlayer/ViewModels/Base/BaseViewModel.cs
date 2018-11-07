using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    /// <summary>
    /// A base view model that fire the Property Changed event
    /// </summary>

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fire when any property when a property changes value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the Property Changed event to force the UI to update
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
