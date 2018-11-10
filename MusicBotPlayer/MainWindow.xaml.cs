using CSharp_SpotifyAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicBotPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SideMenuButton> buttons = new List<SideMenuButton>();

        private ManualResetEvent spotifyAuthenticationCompleted;

        public MainWindow()
        {
            InitializeComponent();

            InitialiseSpotifyApi();

            // Add the side menu buttons to the buttons list for later use.
            buttons.Add(YouTubeButton);
            buttons.Add(SpotifyButton);
        }

        private void InitialiseSpotifyApi()
        {
            Spotify Spotify = new Spotify(); 

            spotifyAuthenticationCompleted = new ManualResetEvent(false);

            Spotify.Api.Authenticated += API_Authenticated;

            //Begin Authentication process, this is run on a different thread
            Spotify.Authenticate(); 

            string authUrl = Spotify.Api.GetAuthenticationUrl();

            browser.Navigated += Browser_Navigated;
            browser.Navigate(authUrl);
            
        }

        private void API_Authenticated(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                browser.Visibility = Visibility.Hidden;
                browser.IsEnabled = false;
            });
        }

        /// <summary>
        /// Fires when a <see cref="SideMenuButton"/> is clicked. Resets the 
        /// IsSelected property on all buttons
        /// </summary>
        /// <param name="sender">The <see cref="SideMenuButton"/></param>
        /// <param name="e"></param>
        private void SideMenuButton_Click(object sender, EventArgs e)
        {
            foreach(SideMenuButton button in buttons)
            {
                button.IsSelected = false;
            }

            SideMenuButton bttn = (SideMenuButton)sender;

            bttn.IsSelected = true;
        }
    }
}
