using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    /// <summary>
    /// Used to store the JSON response for a search query 
    /// to the Spotify API.
    /// </summary>
    [JsonObject]
    public class SearchResult
    {
        public Albums albums { get; set; }
        public Artists artists { get; set; }
        public Tracks tracks { get; set; }
        public Playlists playlists { get; set; }
    }

    [JsonObject]
    public class ExternalUrls
    {
        public string spotify { get; set; }
    }

    [JsonObject]
    public class ArtistSimplified
    {
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    [JsonObject]
    public class Image
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    [JsonObject]
    public class Album : IPage
    {
        public string album_type { get; set; }
        public List<ArtistSimplified> artists { get; set; }
        public List<string> available_markets { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string name { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public int total_tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    [JsonObject]
    public class Albums : IEnumerable<Album>
    {
        public string href { get; set; }
        public List<Album> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }

        /// <summary>
        /// Returns the item List.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Album> GetEnumerator()
        {
            foreach (Album album in items)
            {
                yield return album;
            }
        }

        /// <summary>
        /// Returns the enumerator.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [JsonObject]
    public class Followers
    {
        public object href { get; set; }
        public int total { get; set; }
    }

    [JsonObject]
    public class Artist : IPage
    {
        public ExternalUrls external_urls { get; set; }
        public Followers followers { get; set; }
        public List<object> genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<object> images { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    [JsonObject]
    public class Artists : IEnumerable<Artist>
    {
        public string href { get; set; }
        public List<Artist> items { get; set; }
        public int limit { get; set; }
        public object next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }

        public IEnumerator<Artist> GetEnumerator()
        {
            foreach (Artist artist in items)
            {
                yield return artist;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    [JsonObject]
    public class ExternalIds
    {
        public string isrc { get; set; }
    }

    [JsonObject]
    public class Track : IPage
    {
        public Album album { get; set; }
        public List<ArtistSimplified> artists { get; set; }
        public List<string> available_markets { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool @explicit { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public bool is_local { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    [JsonObject]
    public class Tracks : IEnumerable<Track>
    {
        public string href { get; set; }
        public List<Track> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }

        public IEnumerator<Track> GetEnumerator()
        {
            foreach (Track track in items)
            {
                yield return track;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    [JsonObject]
    public class ImageNullable
    {
        public int? height { get; set; }
        public string url { get; set; }
        public int? width { get; set; }
    }

    [JsonObject]
    public class Owner
    {
        public string display_name { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    [JsonObject]
    public class Tracks2
    {
        public string href { get; set; }
        public int total { get; set; }
    }

    [JsonObject]
    public class Playlist : IPage
    {
        public bool collaborative { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<ImageNullable> images { get; set; }
        public string name { get; set; }
        public Owner owner { get; set; }
        public object primary_color { get; set; }
        public object @public { get; set; }
        public string snapshot_id { get; set; }
        public Tracks2 tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    [JsonObject]
    public class Playlists : IEnumerable<Playlist>
    {
        public string href { get; set; }
        public List<Playlist> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }

        public IEnumerator<Playlist> GetEnumerator()
        {
            foreach (Playlist playlist in items)
            {
                yield return playlist;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
