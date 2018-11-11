using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public partial class SearchResult
    {
        [JsonProperty("albums")]
        public Albums Albums { get; set; }

        [JsonProperty("artists")]
        public Artists Artists { get; set; }

        [JsonProperty("tracks")]
        public Tracks Tracks { get; set; }

        [JsonProperty("playlists")]
        public Albums Playlists { get; set; }
    }

    public partial class Albums
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("items")]
        public AlbumElement[] Items { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class AlbumElement
    {
        [JsonProperty("album_type")]
        public AlbumTypeEnum AlbumType { get; set; }

        [JsonProperty("artists")]
        public Artist[] Artists { get; set; }

        [JsonProperty("available_markets")]
        public string[] AvailableMarkets { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("id")]
        public AlbumId Id { get; set; }

        [JsonProperty("images")]
        public Image[] Images { get; set; }

        [JsonProperty("name")]
        public AlbumName Name { get; set; }

        [JsonProperty("release_date")]
        public ReleaseDate ReleaseDate { get; set; }

        [JsonProperty("release_date_precision")]
        public ReleaseDatePrecision ReleaseDatePrecision { get; set; }

        [JsonProperty("total_tracks")]
        public long TotalTracks { get; set; }

        [JsonProperty("type")]
        public AlbumTypeEnum Type { get; set; }

        [JsonProperty("uri")]
        public AlbumUri Uri { get; set; }
    }

    public partial class Artist
    {
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("id")]
        public ArtistId Id { get; set; }

        [JsonProperty("name")]
        public ArtistName Name { get; set; }

        [JsonProperty("type")]
        public ArtistType Type { get; set; }

        [JsonProperty("uri")]
        public ArtistUri Uri { get; set; }
    }

    public partial class ExternalUrls
    {
        [JsonProperty("spotify")]
        public Uri Spotify { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class Artists
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("items")]
        public ArtistsItem[] Items { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class ArtistsItem
    {
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("followers")]
        public Followers Followers { get; set; }

        [JsonProperty("genres")]
        public object[] Genres { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("id")]
        public ArtistId Id { get; set; }

        [JsonProperty("images")]
        public Image[] Images { get; set; }

        [JsonProperty("name")]
        public ArtistName Name { get; set; }

        [JsonProperty("popularity")]
        public long Popularity { get; set; }

        [JsonProperty("type")]
        public ArtistType Type { get; set; }

        [JsonProperty("uri")]
        public ArtistUri Uri { get; set; }
    }

    public partial class Followers
    {
        [JsonProperty("href")]
        public object Href { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class Tracks
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("items")]
        public TracksItem[] Items { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class TracksItem
    {
        [JsonProperty("album")]
        public AlbumElement Album { get; set; }

        [JsonProperty("artists")]
        public Artist[] Artists { get; set; }

        [JsonProperty("available_markets")]
        public string[] AvailableMarkets { get; set; }

        [JsonProperty("disc_number")]
        public long DiscNumber { get; set; }

        [JsonProperty("duration_ms")]
        public long DurationMs { get; set; }

        [JsonProperty("explicit")]
        public bool Explicit { get; set; }

        [JsonProperty("external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public long Popularity { get; set; }

        [JsonProperty("preview_url")]
        public Uri PreviewUrl { get; set; }

        [JsonProperty("track_number")]
        public long TrackNumber { get; set; }

        [JsonProperty("type")]
        public PurpleType Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public partial class ExternalIds
    {
        [JsonProperty("isrc")]
        public string Isrc { get; set; }
    }

    public enum AlbumTypeEnum { Album };

    public enum ArtistId { The08Td7MxkoHQkXnWayd8D6Q, The0AXuMyPvjBGthmsiknR0Dy };

    public enum ArtistName { AndyGordon, TaniaBowra };

    public enum ArtistType { Artist };

    public enum ArtistUri { SpotifyArtist08Td7MxkoHQkXnWayd8D6Q, SpotifyArtist0AXuMyPvjBGthmsiknR0Dy };

    public enum AlbumId { The1FrhRifX9S3SjpkoYg9N81, The6AkEvsycLGftJxYudPjmqK };

    public enum AlbumName { PlaceInTheSun, TheReverentJorfyLive };

    public enum ReleaseDatePrecision { Day, Year };

    public enum AlbumUri { SpotifyAlbum1FrhRifX9S3SjpkoYg9N81, SpotifyAlbum6AkEvsycLGftJxYudPjmqK };

    public enum PurpleType { Track };

    public partial struct ReleaseDate
    {
        public DateTimeOffset? DateTime;
        public long? Integer;

        public static implicit operator ReleaseDate(DateTimeOffset DateTime) => new ReleaseDate { DateTime = DateTime };
        public static implicit operator ReleaseDate(long Integer) => new ReleaseDate { Integer = Integer };
    }

    internal class AlbumTypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AlbumTypeEnum) || t == typeof(AlbumTypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "album")
            {
                return AlbumTypeEnum.Album;
            }
            throw new Exception("Cannot unmarshal type AlbumTypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AlbumTypeEnum)untypedValue;
            if (value == AlbumTypeEnum.Album)
            {
                serializer.Serialize(writer, "album");
                return;
            }
            throw new Exception("Cannot marshal type AlbumTypeEnum");
        }

        public static readonly AlbumTypeEnumConverter Singleton = new AlbumTypeEnumConverter();
    }

    internal class ArtistIdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ArtistId) || t == typeof(ArtistId?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "08td7MxkoHQkXnWAYD8d6Q":
                    return ArtistId.The08Td7MxkoHQkXnWayd8D6Q;
                case "0aXuMYPvjBGthmsiknR0DY":
                    return ArtistId.The0AXuMyPvjBGthmsiknR0Dy;
            }
            throw new Exception("Cannot unmarshal type ArtistId");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ArtistId)untypedValue;
            switch (value)
            {
                case ArtistId.The08Td7MxkoHQkXnWayd8D6Q:
                    serializer.Serialize(writer, "08td7MxkoHQkXnWAYD8d6Q");
                    return;
                case ArtistId.The0AXuMyPvjBGthmsiknR0Dy:
                    serializer.Serialize(writer, "0aXuMYPvjBGthmsiknR0DY");
                    return;
            }
            throw new Exception("Cannot marshal type ArtistId");
        }

        public static readonly ArtistIdConverter Singleton = new ArtistIdConverter();
    }

    internal class ArtistNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ArtistName) || t == typeof(ArtistName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Andy Gordon":
                    return ArtistName.AndyGordon;
                case "Tania Bowra":
                    return ArtistName.TaniaBowra;
            }
            throw new Exception("Cannot unmarshal type ArtistName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ArtistName)untypedValue;
            switch (value)
            {
                case ArtistName.AndyGordon:
                    serializer.Serialize(writer, "Andy Gordon");
                    return;
                case ArtistName.TaniaBowra:
                    serializer.Serialize(writer, "Tania Bowra");
                    return;
            }
            throw new Exception("Cannot marshal type ArtistName");
        }

        public static readonly ArtistNameConverter Singleton = new ArtistNameConverter();
    }

    internal class ArtistTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ArtistType) || t == typeof(ArtistType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "artist")
            {
                return ArtistType.Artist;
            }
            throw new Exception("Cannot unmarshal type ArtistType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ArtistType)untypedValue;
            if (value == ArtistType.Artist)
            {
                serializer.Serialize(writer, "artist");
                return;
            }
            throw new Exception("Cannot marshal type ArtistType");
        }

        public static readonly ArtistTypeConverter Singleton = new ArtistTypeConverter();
    }

    internal class ArtistUriConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ArtistUri) || t == typeof(ArtistUri?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "spotify:artist:08td7MxkoHQkXnWAYD8d6Q":
                    return ArtistUri.SpotifyArtist08Td7MxkoHQkXnWayd8D6Q;
                case "spotify:artist:0aXuMYPvjBGthmsiknR0DY":
                    return ArtistUri.SpotifyArtist0AXuMyPvjBGthmsiknR0Dy;
            }
            throw new Exception("Cannot unmarshal type ArtistUri");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ArtistUri)untypedValue;
            switch (value)
            {
                case ArtistUri.SpotifyArtist08Td7MxkoHQkXnWayd8D6Q:
                    serializer.Serialize(writer, "spotify:artist:08td7MxkoHQkXnWAYD8d6Q");
                    return;
                case ArtistUri.SpotifyArtist0AXuMyPvjBGthmsiknR0Dy:
                    serializer.Serialize(writer, "spotify:artist:0aXuMYPvjBGthmsiknR0DY");
                    return;
            }
            throw new Exception("Cannot marshal type ArtistUri");
        }

        public static readonly ArtistUriConverter Singleton = new ArtistUriConverter();
    }

    internal class AlbumIdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AlbumId) || t == typeof(AlbumId?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "1FrhRifX9s3sjpkoYG9N81":
                    return AlbumId.The1FrhRifX9S3SjpkoYg9N81;
                case "6akEvsycLGftJxYudPjmqK":
                    return AlbumId.The6AkEvsycLGftJxYudPjmqK;
            }
            throw new Exception("Cannot unmarshal type AlbumId");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AlbumId)untypedValue;
            switch (value)
            {
                case AlbumId.The1FrhRifX9S3SjpkoYg9N81:
                    serializer.Serialize(writer, "1FrhRifX9s3sjpkoYG9N81");
                    return;
                case AlbumId.The6AkEvsycLGftJxYudPjmqK:
                    serializer.Serialize(writer, "6akEvsycLGftJxYudPjmqK");
                    return;
            }
            throw new Exception("Cannot marshal type AlbumId");
        }

        public static readonly AlbumIdConverter Singleton = new AlbumIdConverter();
    }

    internal class AlbumNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AlbumName) || t == typeof(AlbumName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Place In The Sun":
                    return AlbumName.PlaceInTheSun;
                case "The Reverent Jorfy (Live)":
                    return AlbumName.TheReverentJorfyLive;
            }
            throw new Exception("Cannot unmarshal type AlbumName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AlbumName)untypedValue;
            switch (value)
            {
                case AlbumName.PlaceInTheSun:
                    serializer.Serialize(writer, "Place In The Sun");
                    return;
                case AlbumName.TheReverentJorfyLive:
                    serializer.Serialize(writer, "The Reverent Jorfy (Live)");
                    return;
            }
            throw new Exception("Cannot marshal type AlbumName");
        }

        public static readonly AlbumNameConverter Singleton = new AlbumNameConverter();
    }

    internal class ReleaseDateConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ReleaseDate) || t == typeof(ReleaseDate?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    DateTimeOffset dt;
                    if (DateTimeOffset.TryParse(stringValue, out dt))
                    {
                        return new ReleaseDate { DateTime = dt };
                    }
                    long l;
                    if (Int64.TryParse(stringValue, out l))
                    {
                        return new ReleaseDate { Integer = l };
                    }
                    break;
            }
            throw new Exception("Cannot unmarshal type ReleaseDate");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ReleaseDate)untypedValue;
            if (value.DateTime != null)
            {
                serializer.Serialize(writer, value.DateTime.Value.ToString("o", System.Globalization.CultureInfo.InvariantCulture));
                return;
            }
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value.ToString());
                return;
            }
            throw new Exception("Cannot marshal type ReleaseDate");
        }

        public static readonly ReleaseDateConverter Singleton = new ReleaseDateConverter();
    }

    internal class ReleaseDatePrecisionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ReleaseDatePrecision) || t == typeof(ReleaseDatePrecision?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "day":
                    return ReleaseDatePrecision.Day;
                case "year":
                    return ReleaseDatePrecision.Year;
            }
            throw new Exception("Cannot unmarshal type ReleaseDatePrecision");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ReleaseDatePrecision)untypedValue;
            switch (value)
            {
                case ReleaseDatePrecision.Day:
                    serializer.Serialize(writer, "day");
                    return;
                case ReleaseDatePrecision.Year:
                    serializer.Serialize(writer, "year");
                    return;
            }
            throw new Exception("Cannot marshal type ReleaseDatePrecision");
        }

        public static readonly ReleaseDatePrecisionConverter Singleton = new ReleaseDatePrecisionConverter();
    }

    internal class AlbumUriConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AlbumUri) || t == typeof(AlbumUri?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "spotify:album:1FrhRifX9s3sjpkoYG9N81":
                    return AlbumUri.SpotifyAlbum1FrhRifX9S3SjpkoYg9N81;
                case "spotify:album:6akEvsycLGftJxYudPjmqK":
                    return AlbumUri.SpotifyAlbum6AkEvsycLGftJxYudPjmqK;
            }
            throw new Exception("Cannot unmarshal type AlbumUri");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AlbumUri)untypedValue;
            switch (value)
            {
                case AlbumUri.SpotifyAlbum1FrhRifX9S3SjpkoYg9N81:
                    serializer.Serialize(writer, "spotify:album:1FrhRifX9s3sjpkoYG9N81");
                    return;
                case AlbumUri.SpotifyAlbum6AkEvsycLGftJxYudPjmqK:
                    serializer.Serialize(writer, "spotify:album:6akEvsycLGftJxYudPjmqK");
                    return;
            }
            throw new Exception("Cannot marshal type AlbumUri");
        }

        public static readonly AlbumUriConverter Singleton = new AlbumUriConverter();
    }

    internal class PurpleTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PurpleType) || t == typeof(PurpleType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "track")
            {
                return PurpleType.Track;
            }
            throw new Exception("Cannot unmarshal type PurpleType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PurpleType)untypedValue;
            if (value == PurpleType.Track)
            {
                serializer.Serialize(writer, "track");
                return;
            }
            throw new Exception("Cannot marshal type PurpleType");
        }

        public static readonly PurpleTypeConverter Singleton = new PurpleTypeConverter();
    }
}
