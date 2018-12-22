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
    /// Interaction logic for YoutubeSearchItem.xaml
    /// </summary>
    public partial class YoutubeSearchItem : UserControl
    {
        /// <summary>
        /// Exposes the Click event on the button.
        /// </summary>
        public virtual event EventHandler PlayClick;

        public YoutubeSearchItem()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        #region VideoName Dependency Property

        /// <summary>
        /// Stores the text for Video's name.
        /// </summary>
        public string VideoName
        {
            get { return (string)GetValue(VideoNameProperty); }
            set
            {
                SetValue(VideoNameProperty, value);
            }
        }

        /// <summary>
        /// The VideoName dependency property.
        /// </summary>
        public static readonly DependencyProperty VideoNameProperty =
            DependencyProperty.Register("VideoName", typeof(string),
              typeof(YoutubeSearchItem), new PropertyMetadata(null));

        #endregion

        #region ChannelName Dependency Property

        /// <summary>
        /// Stores the text for the channel's name.
        /// </summary>
        public string ChannelName
        {
            get { return (string)GetValue(ChannelNameProperty); }
            set
            {
                SetValue(ChannelNameProperty, value);
            }
        }

        /// <summary>
        /// The ChannelName dependency property.
        /// </summary>
        public static readonly DependencyProperty ChannelNameProperty =
            DependencyProperty.Register("ChannelName", typeof(string),
              typeof(YoutubeSearchItem), new PropertyMetadata(null));

        #endregion

        #region VideoDuration Dependency Property

        /// <summary>
        /// Stores the text for video's duration.
        /// </summary>
        public string VideoDuration
        {
            get { return (string)GetValue(VideoDurationProperty); }
            set
            {
                SetValue(VideoDurationProperty, value);
            }
        }

        /// <summary>
        /// The VideoDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty VideoDurationProperty =
            DependencyProperty.Register("VideoDuration", typeof(string),
              typeof(YoutubeSearchItem), new PropertyMetadata(null));

        #endregion

        #region VideoDescription Dependency Property

        /// <summary>
        /// Stores the text for video's description..
        /// </summary>
        public string VideoDescription
        {
            get { return (string)GetValue(VideoDescriptionProperty); }
            set
            {
                SetValue(VideoDescriptionProperty, value);
            }
        }

        /// <summary>
        /// The VideoDescription dependency property.
        /// </summary>
        public static readonly DependencyProperty VideoDescriptionProperty =
            DependencyProperty.Register("VideoDescription", typeof(string),
              typeof(YoutubeSearchItem), new PropertyMetadata(null));

        #endregion

        #region VideoImage Dependency Property

        /// <summary>
        /// Store the URL to the video image thumbnail.
        /// </summary>
        public string VideoImage
        {
            get { return (string)GetValue(VideoImageProperty); }
            set
            {
                SetValue(VideoImageProperty, value);
            }
        }

        /// <summary>
        /// The VideoImage dependency property.
        /// </summary>
        public static readonly DependencyProperty VideoImageProperty =
            DependencyProperty.Register("VideoImage", typeof(string),
              typeof(YoutubeSearchItem), new PropertyMetadata(null));

        #endregion

        #endregion

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayClick?.Invoke(this, e);
        }
    }
}
