using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for TrackSearchItem.xaml
    /// </summary>
    public partial class TrackSearchItem : UserControl
    {
        /// <summary>
        /// Exposes the Click event on the button.
        /// </summary>
        public virtual event EventHandler PlayButtonClick;

        public TrackSearchItem()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        #region TrackName Dependency Property

        /// <summary>
        /// Stores the text for Track's name.
        /// </summary>
        public string TrackName
        {
            get { return (string)GetValue(TrackNameProperty); }
            set
            {
                SetValue(TrackNameProperty, value);
            }
        }

        /// <summary>
        /// The TrackName dependency property.
        /// </summary>
        public static readonly DependencyProperty TrackNameProperty =
            DependencyProperty.Register("TrackName", typeof(string),
              typeof(TrackSearchItem), new PropertyMetadata(null));

        #endregion

        #region TrackArtists Dependency Property

        /// <summary>
        /// Stores the text for Track's artists.
        /// </summary>
        public string TrackArtists
        {
            get { return (string)GetValue(TrackArtistsProperty); }
            set
            {
                SetValue(TrackArtistsProperty, value);
            }
        }

        /// <summary>
        /// The TrackArtists dependency property.
        /// </summary>
        public static readonly DependencyProperty TrackArtistsProperty =
            DependencyProperty.Register("TrackArtists", typeof(string),
              typeof(TrackSearchItem), new PropertyMetadata(null));

        #endregion

        #region TrackLength Dependency Property

        /// <summary>
        /// Stores the text for Track's length.
        /// </summary>
        public string TrackLength
        {
            get { return (string)GetValue(TrackLengthProperty); }
            set
            {
                SetValue(TrackLengthProperty, value);
            }
        }

        /// <summary>
        /// The TrackLength dependency property.
        /// </summary>
        public static readonly DependencyProperty TrackLengthProperty =
            DependencyProperty.Register("TrackLength", typeof(string),
              typeof(TrackSearchItem), new PropertyMetadata(null));

        #endregion

        #endregion

        /// <summary>
        /// Fires when the play button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButtonClick?.Invoke(this, e);
        }
    }
}
