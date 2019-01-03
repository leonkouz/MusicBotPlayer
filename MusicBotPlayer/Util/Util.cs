using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicBotPlayer
{
    public class Util
    {
        /// <summary>
        /// Adds the items from a specified <see cref="IEnumerable{T}"/> to a <see cref="ObservableCollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ObservableCollection{T}"/></typeparam>
        /// <param name="collectionToAddTo">The collection to add to.</param>
        /// <param name="toAddFrom">The collection of items to add from.</param>
        public static void AddToCollection<T>(ObservableCollection<T> collectionToAddTo, IEnumerable<object> toAddFrom)
        {
            foreach (object obj in toAddFrom)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    collectionToAddTo.Add((T)obj);
                });
            }
        }
    }
}