using CSharp_SpotifyAPI;
using CSharp_SpotifyAPI.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public class Spotify
    {
        /// <summary>
        /// The Spotify API.
        /// </summary>
        public static SpotifyAPI Api { get; private set; }

        /// <summary>
        /// List of all search types.
        /// </summary>
        public static List<SearchType> AllSearchTypes = new List<SearchType>()
            {
                SearchType.album,
                SearchType.artist,
                SearchType.playlist,
                SearchType.track,
            };

        /// <summary>
        /// Initialises the Spotify API.
        /// </summary>
        public Spotify()
        {
            string clientID = ApiKeys.GetSpotifyClientIdFromAppData();
            string redirectID = "http%3A%2F%2Flocalhost%3A62177";
            string state = "123";

            List<Scope> scope = new List<Scope>()
            {
                Scope.UserReadPrivate,
                Scope.UserReadBirthdate,
                Scope.UserModifyPlaybackState,
                Scope.UserModifyPlaybackState,
                Scope.UserFollowRead, Scope.UserFollowModify,
                Scope.UserReadRecentlyPlayed,
                Scope.UserReadPlaybackState
            };

            Api = new SpotifyAPI(clientID, redirectID, state, scope, true);
        }

        /// <summary>
        /// Creates a new thread and authenticates the API.
        /// </summary>
        public void Authenticate()
        {
            Thread authentication = new Thread(new ThreadStart(DoAuthentication));
            authentication.Start();
        }

        /// <summary>
        /// Runs the authentication workflow.
        /// </summary>
        private void DoAuthentication()
        {
            Api.Authenticate(false);
        }

        /// <summary>
        /// Gets the image of the track.
        /// </summary>
        /// <param name="id">The id of the track.</param>
        /// <returns></returns>
        public static string GetTrackImage(string id)
        {
            TrackSearchResult result = JsonConvert.DeserializeObject<TrackSearchResult>(Spotify.Api.GetTrack(id));
            string image = result.album.images[2].url;

            return image;
        }
    }
}
