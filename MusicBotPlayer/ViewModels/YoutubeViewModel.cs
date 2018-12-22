using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public class YoutubeViewModel : BaseViewModel
    {
        /// <summary>
        /// Indicates whether the View Model is selected.
        /// </summary>
        private bool isSelected = false;

        /// <summary>
        /// Stores the list of youtube search results.
        /// </summary>
        private ObservableCollection<Google.Apis.YouTube.v3.Data.Video> youtubeSearchResults = new ObservableCollection<Google.Apis.YouTube.v3.Data.Video>();

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
        /// Stores the list of youtube search results.
        /// </summary>
        public ObservableCollection<Google.Apis.YouTube.v3.Data.Video> YoutubeSearchResults
        {
            get => youtubeSearchResults;
            set
            {
                youtubeSearchResults = value;
            }
        }

        public YoutubeViewModel()
        {

        }

        /// <summary>
        /// Search the Youtube API for the specified search term 
        /// and populate the YoutubeSearchResults collection.
        /// </summary>
        /// <param name="searchTerm"></param>
        public async void Search(string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
            {
                return;
            }

            if(YoutubeSearchResults.Count >= 0)
            {
                YoutubeSearchResults.Clear();
            }

            await Task.Run(() =>
            {
                var results = YoutubeApi.GetSearchResults(searchTerm);

                App.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var video in results)
                    {
                        video.ContentDetails.Duration = StringHelper.ConvertISO8601DurationToString(video.ContentDetails.Duration);
                        YoutubeSearchResults.Add(video);
                    }
                });
            });
        }
    }
}
