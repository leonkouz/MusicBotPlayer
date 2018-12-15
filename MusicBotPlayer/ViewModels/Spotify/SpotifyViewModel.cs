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
        /// Stores the search results for albums.
        /// </summary>
        private ObservableCollection<Album> albums = new ObservableCollection<Album>();
        
        /// <summary>
        /// Stores the search results for tracks.
        /// </summary>
        private ObservableCollection<Track> tracks = new ObservableCollection<Track>();

        /// <summary>
        /// Stores the search results for playlists.
        /// </summary>
        private ObservableCollection<Playlist> playlists = new ObservableCollection<Playlist>();

        /// <summary>
        /// Stores the search results for artists.
        /// </summary>
        private ObservableCollection<Artist> artists = new ObservableCollection<Artist>();

        /// <summary>
        /// Stores the tracks of an album. Used to display the tracks of an 
        /// album when it is clicked on.
        /// </summary>
        private ObservableCollection<AblumTrackSearchTrack> albumsTracks = new ObservableCollection<AblumTrackSearchTrack>();

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

        /// <summary>
        /// Stores the search results from albums.
        /// </summary>
        public ObservableCollection<Album> Albums
        {
            get => albums;
            set
            {
                albums = value;
            }
        }
        
        /// <summary>
        /// Stores the search results for tracks.
        /// </summary>
        public ObservableCollection<Track> Tracks
        {
            get => tracks;
            set
            {
                tracks = value;
            }
        }

        /// <summary>
        /// Stores the search results for playlists.
        /// </summary>
        public ObservableCollection<Playlist> Playlists
        {
            get => playlists;
            set
            {
                playlists = value;
            }
        }

        /// <summary>
        /// Stores the search results for artists.
        /// </summary>
        public ObservableCollection<Artist> Artists
        {
            get => artists;
            set
            {
                artists = value;
            }
        }

        /// <summary>
        /// Stores the tracks of an album. Used to display the tracks of an 
        /// album when it is clicked on.
        /// </summary>
        public ObservableCollection<AblumTrackSearchTrack> AlbumsTracks
        {
            get => albumsTracks;
            set
            {
                albumsTracks = value;
            }
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public SpotifyViewModel()
        {
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
                        ClearCurrentSearch();
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
                AddToCollection(Albums, results.albums);
                AddToCollection(Tracks, results.tracks.OrderByDescending(x => x.popularity));
                AddToCollection(Playlists, results.playlists);
                AddToCollection(Artists, results.artists);
            });
        }

        /// <summary>
        /// Adds the items from a specified <see cref="IEnumerable{T}"/> to a <see cref="ObservableCollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ObservableCollection{T}"/></typeparam>
        /// <param name="collectionToAddTo">The collection to add to.</param>
        /// <param name="toAddFrom">The collection of items to add from.</param>
        public void AddToCollection<T>(ObservableCollection<T> collectionToAddTo, IEnumerable<object> toAddFrom)
        {
            foreach(object obj in toAddFrom)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    collectionToAddTo.Add((T)obj);
                });
            }
        }

        /// <summary>
        /// Clears the current search collections.
        /// </summary>
        private void ClearCurrentSearch()
        {
            Albums.Clear();
            AlbumsTracks.Clear();
            Tracks.Clear();
            Playlists.Clear();
            Artists.Clear();
            AlbumsTracks.Clear();
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
                    AlbumsTracks.Clear();
                });

                AlbumTrackSearchResult album = JsonConvert.DeserializeObject<AlbumTrackSearchResult>(Spotify.Api.GetAlbumTracks(id, 50, 0));

                if(album.items.Count == 0)
                {
                    return;
                }

                AddToCollection(AlbumsTracks, album.items);
            });
        }
    }
}
