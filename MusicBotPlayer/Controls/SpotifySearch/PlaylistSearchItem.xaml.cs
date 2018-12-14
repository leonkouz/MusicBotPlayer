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
    /// Interaction logic for PlaylistSearchItem.xaml
    /// </summary>
    public partial class PlaylistSearchItem : UserControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PlaylistSearchItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Exposes the Click event on the button.
        /// </summary>
        public virtual event EventHandler Click;

        /// <summary>
        /// Fires when the control is clicked on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaylistSearchItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        #region Dependency Properties

        #region PlaylistName Dependency Property

        /// <summary>
        /// Stores the text for Playlist name
        /// </summary>
        public string PlaylistName
        {
            get { return (string)GetValue(PlaylistNameProperty); }
            set
            {
                SetValue(PlaylistNameProperty, value);
            }
        }

        /// <summary>
        /// The PlaylistName dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaylistNameProperty =
            DependencyProperty.Register("PlaylistName", typeof(string),
              typeof(PlaylistSearchItem), new PropertyMetadata(null));

        #endregion

        #region PlaylistImage Dependency Property

        /// <summary>
        /// Stores the URL for the playlist image.
        /// </summary>
        public string PlaylistImage
        {
            get { return (string)GetValue(PlaylistImageProperty); }
            set
            {
                SetValue(PlaylistImageProperty, value);
            }
        }

        /// <summary>
        /// The AlbumName dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaylistImageProperty =
            DependencyProperty.Register("PlaylistImage", typeof(string),
              typeof(PlaylistSearchItem), new PropertyMetadata(null));

        #endregion

        #region PlaylistId Dependency Property

        /// <summary>
        /// Stores the text for the playlist ID.
        /// </summary>
        public string PlaylistId
        {
            get { return (string)GetValue(PlaylistIdProperty); }
            set
            {
                SetValue(PlaylistIdProperty, value);
            }
        }

        /// <summary>
        /// The PlaylistId dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaylistIdProperty =
            DependencyProperty.Register("PlaylistId", typeof(string),
              typeof(PlaylistSearchItem), new PropertyMetadata(null));

        #endregion

        #region PlaylistTrackCount Dependency Property

        /// <summary>
        /// Stores the playlists's track count.
        /// </summary>
        public string PlaylistTrackCount
        {
            get { return (string)GetValue(PlaylistTrackCountProperty); }
            set
            {
                SetValue(PlaylistTrackCountProperty, value);
            }
        }

        /// <summary>
        /// The PlaylistTrackCount dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaylistTrackCountProperty =
            DependencyProperty.Register("PlaylistTrackCount", typeof(string),
              typeof(PlaylistSearchItem), new PropertyMetadata(null));

        #endregion

        #endregion
    }
}
