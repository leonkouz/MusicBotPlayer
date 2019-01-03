using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicBotPlayer
{
    [JsonObject]
    public class PlaylistTrackSearchResult
    {
        public string href { get; set; }
        public List<PlaylistTrackSearchDetails> items { get; set; }
        public int limit { get; set; }
        public object next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }

    [JsonObject]
    public class AddedBy
    {
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    [JsonObject]
    public class PlaylistTrackSearchTrack
    {
        public Album album { get; set; }
        public List<ArtistSimplified> artists { get; set; }
        public List<object> available_markets { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool episode { get; set; }
        public bool @explicit { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public bool is_local { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public bool track { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    [JsonObject]
    public class VideoThumbnail
    {
        public object url { get; set; }
    }

    [JsonObject]
    public class PlaylistTrackSearchDetails : IPage
    {
        public DateTime added_at { get; set; }
        public AddedBy added_by { get; set; }
        public bool is_local { get; set; }
        public object primary_color { get; set; }
        public PlaylistTrackSearchTrack track { get; set; }
        public VideoThumbnail video_thumbnail { get; set; }
    }
}