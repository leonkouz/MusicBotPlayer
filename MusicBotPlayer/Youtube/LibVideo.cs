using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace MusicBotPlayer
{
    public class LibVideo
    {
        public static string GetLink(string url)
        {
            var youTube = YouTube.Default; // starting point for YouTube actions
            var video = youTube.GetVideo(url); // gets a Video object with info about the video

            return video.Uri;
        }

    }
}
