using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicBotPlayer
{
    public class ApiKeys
    {
        /// <summary>
        /// Read the Spotify.txt file for the Spotify API client Id in %appdata%\MusicBotPlayerCache.
        /// </summary>
        /// <returns>The Spotify client ID.</returns>
        public static string GetSpotifyClientIdFromAppData()
        {
            string appDataPathTextFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MusicBotPlayerCache\\SpotifyToken.txt";

            if (File.Exists(appDataPathTextFile))
            {
                string text = File.ReadAllText(appDataPathTextFile);

                return text;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Read the DiscordToken.txt file for the Discord Bot token in %appdata%\MusicBotPlayerCache.
        /// </summary>
        /// <returns>The Discord bot token.</returns>
        public static string GetDiscordBotTokenFromAppData()
        {
            string appDataPathTextFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MusicBotPlayerCache\\DiscordToken.txt";

            if (File.Exists(appDataPathTextFile))
            {
                string text = File.ReadAllText(appDataPathTextFile);

                return text;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Read the YoutubeToken.txt file for the youtube token in %appdata%\MusicBotPlayerCache.
        /// </summary>
        /// <returns>The Discord bot token.</returns>
        public static string GetYoutubeTokenFromAppData()
        {
            string appDataPathTextFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MusicBotPlayerCache\\YoutubeToken.txt";

            if (File.Exists(appDataPathTextFile))
            {
                string text = File.ReadAllText(appDataPathTextFile);

                return text;
            }
            else
            {
                return null;
            }
        }
    }
}