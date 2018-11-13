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
        /// <summary>
        /// The Side Menu buttons.
        /// </summary>
        private List<SideMenuButton> buttons = new List<SideMenuButton>();

        /// <summary>
        /// The <see cref="ManualResetEvent"/> for the spotify authentication process.
        /// </summary>
        private ManualResetEvent spotifyAuthenticationCompleted;

        /// <summary>
        /// The Application View Model.
        /// </summary>
        private ApplicationViewModel viewModel;

        /// <summary>
        /// Indicates whether the Spotify API has authenticated yet.
        /// </summary>
        private bool spotifyAuthenticated;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            viewModel = (ApplicationViewModel)this.DataContext;

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

            browser.Navigate(authUrl);
        }

        /// <summary>
        /// Fires when the Spotify API has authenticated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void API_Authenticated(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                spotifyAuthenticated = true;
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

        /// <summary>
        /// Fires when a key is pressed while the Search Bar is in focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = sender as TextBox;

            // If the enter key is pressed and the Spotify API has authenticated, 
            // do a search.
            if(e.Key == Key.Enter && spotifyAuthenticated == true)
            {
                viewModel.SpotifyViewModel.Search(txtBox.Text);
            }
        }

       
    }
}
