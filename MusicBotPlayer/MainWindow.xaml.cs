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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SideMenuButton> buttons = new List<SideMenuButton>();

        public MainWindow()
        {
            InitializeComponent();

            // Add the side menu buttons to the buttons list for later use.
            buttons.Add(YouTubeButton);
            buttons.Add(SpotifyButton);
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
