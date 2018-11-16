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
    /// Interaction logic for AlbumSearchItem.xaml
    /// </summary>
    public partial class AlbumSearchItem : UserControl
    {
        /// <summary>
        /// Exposes the Click event on the button.
        /// </summary>
        public virtual event EventHandler Click;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AlbumSearchItem()
        {
            this.DataContext = this;

            InitializeComponent();
        }

        /// <summary>
        /// Fires when the control is clicked on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        #region Dependency Properties

        #region ArtistName Dependency Property

        /// <summary>
        /// Stores the text for Artist names
        /// </summary>
        public string ArtistNames
        {
            get { return (string)GetValue(ArtistNamesProperty); }
            set
            {
                SetValue(ArtistNamesProperty, value);
            }
        }

        /// <summary>
        /// The ArtistNames dependency property.
        /// </summary>
        public static readonly DependencyProperty ArtistNamesProperty =
            DependencyProperty.Register("ArtistNames", typeof(string),
              typeof(AlbumSearchItem), new PropertyMetadata(null));

        #endregion

        #region AlbumName Dependency Property

        /// <summary>
        /// Stores the text for the album name.
        /// </summary>
        public string AlbumName
        {
            get { return (string)GetValue(AlbumNameProperty); }
            set
            {
                SetValue(AlbumNameProperty, value);
            }
        }

        /// <summary>
        /// The AlbumName dependency property.
        /// </summary>
        public static readonly DependencyProperty AlbumNameProperty =
            DependencyProperty.Register("AlbumName", typeof(string),
              typeof(AlbumSearchItem), new PropertyMetadata(null));

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
              typeof(AlbumSearchItem), new PropertyMetadata(null));

        #endregion

        #endregion
    }
}
