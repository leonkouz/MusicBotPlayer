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
    /// Interaction logic for QueueItem.xaml
    /// </summary>
    public partial class QueueItem : UserControl
    {
        /// <summary>
        /// Exposes the Click event on the button.
        /// </summary>
        public virtual event EventHandler DeleteButtonClick;

        #region Dependecy Properties

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
              typeof(QueueItem), new PropertyMetadata(null));

        #endregion

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
              typeof(QueueItem), new PropertyMetadata(null));

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
              typeof(QueueItem), new PropertyMetadata(null));

        #endregion

        #region AlbumImage Dependency Property

        /// <summary>
        /// Stores the text for the album name.
        /// </summary>
        public string AlbumImage
        {
            get { return (string)GetValue(AlbumImageProperty); }
            set
            {
                SetValue(AlbumImageProperty, value);
            }
        }

        /// <summary>
        /// The AlbumName dependency property.
        /// </summary>
        public static readonly DependencyProperty AlbumImageProperty =
            DependencyProperty.Register("AlbumImage", typeof(string),
              typeof(QueueItem), new PropertyMetadata(null));

        #endregion

        #region ProgressPercentage Dependency Property

        /// <summary>
        /// Stores the text for the progress percentage of the download.
        /// </summary>
        public string ProgressPercentage
        {
            get { return (string)GetValue(ProgressPercentageProperty); }
            set
            {
                SetValue(ProgressPercentageProperty, value);
            }
        }

        /// <summary>
        /// The ProgressPercentage dependency property.
        /// </summary>
        public static readonly DependencyProperty ProgressPercentageProperty =
            DependencyProperty.Register("ProgressPercentage", typeof(string),
              typeof(QueueItem), new PropertyMetadata(null));

        #endregion

        #endregion

        public QueueItem()
        {
            InitializeComponent();
        }

        private void DeleteButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DeleteButtonClick?.Invoke(this, e);
        }
    }
}
