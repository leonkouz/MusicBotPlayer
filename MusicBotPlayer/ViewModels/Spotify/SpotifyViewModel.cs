using CSharp_SpotifyAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace MusicBotPlayer
{
    public class SpotifyViewModel : BaseViewModel
    {
        #region Private Fields
        
        /// <summary>
        /// Indicates whether the View Model is selected.
        /// </summary>
        private bool isSelected = false;

        /// <summary>
        /// Holds the current Search Result from the Spotify API.
        /// </summary>
        private SearchResult searchResult;

        /// <summary>
        /// Indicates if the search results are currently being loaded.
        /// </summary>
        private bool isLoadingSearchResults = false;

        /// <summary>
        /// The page controller. 
        /// </summary>
        private PageController pageController;

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if the search results are currently being loaded.
        /// </summary>
        public bool IsLoadingSearchResults
        {
            get => isLoadingSearchResults;
            set
            {
                IsLoadingSearchResults = value;
            }
        }

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

        public PageController PageController
        {
            get => pageController;
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public SpotifyViewModel()
        {
            pageController = new PageController();

            Page albumPage = new Page("AlbumPage");
            albumPage.ChildPage = new Page("AlbumTracksPage");

            Page playlistPage = new Page("PlaylistPage");
            playlistPage.ChildPage = new Page("PlaylistTrackPage");

            Page trackpage = new Page("TracksPage");

            Page artistsPage = new Page("ArtistPage");
            artistsPage.ChildPage = new Page("ArtistAlbumPage");
            artistsPage.ChildPage.ChildPage = new Page("ArtistsAlbumTracksPage");

            pageController.AddPage(albumPage);
            pageController.AddPage(playlistPage);
            pageController.AddPage(trackpage);
            pageController.AddPage(artistsPage);

            pageController.NavigateToPage(albumPage);
        }

        /// <summary>
        /// Fires when a key is pressed while the Search Bar is in focus.
        /// Requests a search for specified text from the Spotify API.
        /// </summary>
        public async void Search(string text)
        {
            if(String.IsNullOrEmpty(text))
            {
                return;
            }

            string searchResponse = String.Empty;
            try
            {
                await Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        pageController.ClearItemsOfAllPages();
                    });

                    searchResponse = Spotify.Api.Search(text, Spotify.AllSearchTypes, 40, 0);
                    searchResult = JsonConvert.DeserializeObject<SearchResult>(searchResponse);
                });
            }
            catch(Exception e)
            {
                MessageBox.Show("Unable to retrieve data from Spotify API: " + e.ToString());
                return;
            }

            LoadSearchResultsToCollections(searchResult);
        }

        /// <summary>
        /// Loads the search results into individual collections for
        /// artists, playlists, tracks and albums.
        /// </summary>
        /// <param name="results">The search results.</param>
        public async void LoadSearchResultsToCollections(SearchResult results)
        {
            await Task.Run(() =>
            {
                Util.AddToCollection(pageController.GetPage("AlbumPage").Items, results.albums);
                Util.AddToCollection(pageController.GetPage("TracksPage").Items, results.tracks.OrderByDescending(x => x.popularity));
                Util.AddToCollection(pageController.GetPage("PlaylistPage").Items, results.playlists);
                Util.AddToCollection(pageController.GetPage("ArtistPage").Items, results.artists);
            });
        }

        /// <summary>
        /// Search for the tracks of the specified album.
        /// </summary>
        /// <param name="id"></param>
        public async void AlbumTrackSearch(string id)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    pageController.GetPage("AlbumTracksPage").Items.Clear();
                });

                AlbumTrackSearchResult album = JsonConvert.DeserializeObject<AlbumTrackSearchResult>(Spotify.Api.GetAlbumTracks(id, 50, 0));

                if(album.items.Count == 0)
                {
                    return;
                }

                Util.AddToCollection(pageController.GetPage("AlbumTracksPage").Items, album.items);
            });
        }

        /// <summary>
        /// Search for the tracks of the specified playlist.
        /// </summary>
        /// <param name="id"></param>
        public async void PlaylistTrackSearch(string id)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    pageController.GetPage("PlaylistTrackPage").Items.Clear();
                });

                PlaylistTrackSearchResult playlist = JsonConvert.DeserializeObject<PlaylistTrackSearchResult>(Spotify.Api.GetPlaylistsTracks(id));

                if (playlist.items.Count == 0)
                {
                    return;
                }

                Util.AddToCollection(pageController.GetPage("PlaylistTrackPage").Items, playlist.items);
            });
        }

    }
}
