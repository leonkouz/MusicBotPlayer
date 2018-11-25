using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBotPlayer
{
    public class YoutubeApi
    {
        public static string YoutubeSearch(string searchTerm)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKeys.GetYoutubeTokenFromAppData(),
                ApplicationName = "MusicBot"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchTerm; //Search Term
            searchListRequest.MaxResults = 50;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance; //set the search to order by relevance
            searchListRequest.Type = "video";

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> videosID = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Result.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    //videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                    videosID.Add(searchResult.Id.VideoId);
                }
            }

            string videoURL;

            if (videosID.Count != 0)
                videoURL = "https://www.youtube.com/watch?v=" + videosID[0]; //gets the first video which should be the most relevant due to ordering set to relevance
            else
                videoURL = null;

            return videoURL;
        }
    }
}
