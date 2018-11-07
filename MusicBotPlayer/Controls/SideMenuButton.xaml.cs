using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for SideMenuButton.xaml
    /// </summary>
    public partial class SideMenuButton : UserControl
    {
        /// <summary>
        /// Exposes the Click event on the button.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SideMenuButton()
        {
            InitializeComponent();

            IsSelected = false;

            this.DataContext = this;

            button.Click += ButtonClick;
        }

        #region Dependency Properties

        #region Text Dependency Property

        /// <summary>
        /// Stores the text for the button's contents field.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// The Text dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
              typeof(SideMenuButton), new PropertyMetadata(null));

        #endregion

        #region IsSelected Dependency Property

        /// <summary>
        /// Indicates if the button is currently selected.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        /// <summary>
        /// The IsSelected dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool),
              typeof(SideMenuButton), new PropertyMetadata(null));

        #endregion

        #region Command Dependency Property

        /// <summary>
        /// Exposes the command property on the button 
        /// to allow commands to be used in the ViewModel
        /// </summary>
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        /// <summary>
        /// The command's dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
           DependencyProperty.Register("Command", typeof(ICommand), typeof(SideMenuButton), new UIPropertyMetadata(null));

        #endregion

        #endregion

        /// <summary>
        /// Exposes the click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
            Click?.Invoke(this, e);
        }
    }
}
