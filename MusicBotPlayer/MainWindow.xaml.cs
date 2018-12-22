using CefSharp;
using CefSharp.Wpf;
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
using System.Windows.Threading;

namespace MusicBotPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Spotify spotify;

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

        private DispatcherTimer sliderTimer = new DispatcherTimer();

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            // Must be run before the brower control is initialised.
            InitialiseCefSharpBrowser();

            // Initialise WPF window component.
            InitializeComponent();

            InitialiseSpotifyApi();
            YoutubeApi.Authenticate();

            viewModel = (ApplicationViewModel)this.DataContext;

            // Add the side menu buttons to the buttons list for later use.
            buttons.Add(YouTubeButton);
            buttons.Add(SpotifyButton);
            buttons.Add(QueueButton);

            this.Closed += MainWindow_Closed;

            // Set Youtube and Queue grid visibility to collapsed so 
            // controls from those grids are not visible on statup.
            YoutubeGrid.Visibility = Visibility.Collapsed;
            QueueGrid.Visibility = Visibility.Collapsed;

            sliderTimer.Interval = TimeSpan.FromSeconds(1);
            sliderTimer.Tick += SliderTimer_Tick;
            sliderTimer.Start();
        }

        /// <summary>
        /// Fires when the application is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            // Kill the authentication thread.
            spotify.KillAuthenticationThread();
        }

        /// <summary>
        /// Update the value of the slider.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderTimer_Tick(object sender, EventArgs e)
        {
            if (viewModel.QueueViewModel.CurrentPointInTrack != null)
            {
                if(viewModel.QueueViewModel.TrackDuration.TotalSeconds != 0 && viewModel.QueueViewModel.CurrentPointInTrack.TotalSeconds != 0)
                {
                    slider.Maximum = viewModel.QueueViewModel.TrackDuration.TotalSeconds;

                    var sliderValue = viewModel.QueueViewModel.CurrentPointInTrack.TotalSeconds;

                    if (slider.Value != sliderValue)
                    {
                        slider.Value = sliderValue;
                    }
                }
            }
        }

        /// <summary>
        /// Initialise CefSharp browser. Must be run before the control is initialised.
        /// </summary>
        private void InitialiseCefSharpBrowser()
        {
            var settings = new CefSettings();
            settings.BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe";
            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MusicBotPlayerCache";

            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
        }

        /// <summary>
        /// Initialise the Spotify API.
        /// </summary>
        private void InitialiseSpotifyApi()
        {
            spotify = new Spotify();

            spotifyAuthenticationCompleted = new ManualResetEvent(false);

            Spotify.Api.Authenticated += API_Authenticated;

            //Begin Authentication process, this is run on a different thread
            spotify.Authenticate();

            string authUrl = Spotify.Api.GetAuthenticationUrl();

            browser.Address = authUrl;

            spotifyAuthenticationCompleted.Dispose();
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
            foreach (SideMenuButton button in buttons)
            {
                button.IsSelected = false;
            }

            SideMenuButton bttn = (SideMenuButton)sender;
            bttn.IsSelected = true;

            if(bttn.Name == "SpotifyButton")
            {
                SpotifyGrid.Visibility = Visibility.Visible;
                viewModel.SpotifyViewModel.IsSelected = true;
            }
            else
            {
                SpotifyGrid.Visibility = Visibility.Collapsed;
                viewModel.SpotifyViewModel.IsSelected = false;
            }

            if (bttn.Name == "YouTubeButton")
            {
                YoutubeGrid.Visibility = Visibility.Visible;
                viewModel.YoutubeViewModel.IsSelected = true;
            }
            else
            {
                YoutubeGrid.Visibility = Visibility.Collapsed;
                viewModel.YoutubeViewModel.IsSelected = false;
            }

            if (bttn.Name == "QueueButton")
            {
                QueueGrid.Visibility = Visibility.Visible;
            }
            else
            {
                QueueGrid.Visibility = Visibility.Collapsed;
            }

        }

        /// <summary>
        /// Fires when a key is pressed while the Spotify Search Bar is in focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpotifySearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = sender as TextBox;

            // If the enter key is pressed and the Spotify API has authenticated, 
            // do a search.
            if (e.Key == Key.Enter && spotifyAuthenticated == true)
            {
                viewModel.SpotifyViewModel.Search(txtBox.Text);
            }
        }

        private void YoutubeSearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = sender as TextBox;

            // If the enter key is pressed and the Spotify API has authenticated, 
            // do a search.
            if (e.Key == Key.Enter && spotifyAuthenticated == true)
            {
                viewModel.YoutubeViewModel.Search(txtBox.Text);
            }
        }

        private void AlbumSearchItem_Click(object sender, EventArgs e)
        {
            AlbumSearchItem album = sender as AlbumSearchItem;
            viewModel.SpotifyViewModel.AlbumTrackSearch(album.AlbumId);

            AlbumListView.Visibility = Visibility.Collapsed;
            AlbumTracksListView.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        private void BackButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AlbumListView.Visibility = Visibility.Visible;
            AlbumTracksListView.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Fires when a Spotify Track item Play button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackSearchItem_Click(object sender, EventArgs e)
        {
            TrackSearchItem track = sender as TrackSearchItem;

            viewModel.QueueViewModel.AddToQueue(track);
        }

        /// <summary>
        /// Fires when a YoutubeSearch item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YoutubeSearchItem_Click(object sender, EventArgs e)
        {
            YoutubeSearchItem youtubeSearchItem = sender as YoutubeSearchItem;

            viewModel.QueueViewModel.AddToQueue(youtubeSearchItem);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        /// <summary>
        /// Fires when the Delete button is clicked on a <see cref="QueueItem"/>.
        /// Removes the clicked <see cref="QueueItem"/> from the queue.
        /// </summary>
        private void QueueItem_DeleteButtonClick(object sender, EventArgs e)
        {
            QueueItem queueItem = (QueueItem)sender;

            QueueTrack track = (QueueTrack)queueItem.DataContext;

            viewModel.QueueViewModel.RemoveFromQueue(track);
        }

    }
}
