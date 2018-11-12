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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicBotPlayer
{
    /// <summary>
    /// Interaction logic for HorizontalMenuBar.xaml
    /// </summary>
    public partial class HorizontalMenuBar : UserControl
    {
        /// <summary>
        /// Stores the list of buttons in the control.
        /// </summary>
        private List<HorizontalMenuButton> buttons = new List<HorizontalMenuButton>();

        /// <summary>
        /// Stores the starting point of the hover border.
        /// </summary>
        private double from = 0;

        /// <summary>
        /// Stores the X offset of the selected button for use by the hover border.
        /// </summary>
        private double selectedButtonOffset = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        public HorizontalMenuBar()
        {
            InitializeComponent();

            // Select first button by default.
            button1.IsSelected = true;

            // Adds each button to the buttons list
            buttons.Add(button1);
            buttons.Add(button2);
            buttons.Add(button3);
            buttons.Add(button4);
        }

        /// <summary>
        /// Moves the hover border to the specified X offset.
        /// </summary>
        /// <param name="xPosition">The X offset.</param>
        private void MoveBorder(double xPosition)
        {
            var to = xPosition;
            TranslateTransform trans = new TranslateTransform();
            canBorder.RenderTransform = trans;
            var be = new BackEase();
            DoubleAnimation da1 = new DoubleAnimation(from, to, TimeSpan.FromMilliseconds(300))
            {
                AccelerationRatio = 1
            };
            trans.BeginAnimation(TranslateTransform.XProperty, da1);
            from = to;
        }

        /// <summary>
        /// Fires when a <see cref="HorizontalMenuButton"/> is clicked.
        /// </summary>
        private void HorizontalMenuButton_Click(object sender, EventArgs e)
        {
            // Sets all button's IsSelected property to false.
            foreach (HorizontalMenuButton button in buttons)
            {
                button.IsSelected = false;
            }

            HorizontalMenuButton bttn = (HorizontalMenuButton)sender;

            bttn.IsSelected = true;

            // Gets the X offset of the clicked button.
            var btn = sender as HorizontalMenuButton;
            double xOffset = GetVisualOffsetX(btn);

            // Stores the clicked buttons offset.
            selectedButtonOffset = xOffset;

            MoveBorder(xOffset);
        }

        /// <summary>
        /// Fires when the mouse enters the button. 
        /// </summary>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            from = GetBorderVisualOffsetX();
            var btn = sender as HorizontalMenuButton;
            double xOffset = GetVisualOffsetX(btn);
            MoveBorder(xOffset);
            from = xOffset;
        }

        /// <summary>
        /// Fires when the mouse leaves the <see cref="HorizontalMenuBar"/>.
        /// </summary>
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            from = GetBorderVisualOffsetX();
            MoveBorder(selectedButtonOffset);
        }

        /// <summary>
        /// Gets the X offset of the specified button.
        /// </summary>
        /// <param name="button">The button to retrieve the X offset from.</param>
        /// <returns></returns>
        private double GetVisualOffsetX(HorizontalMenuButton button)
        {
            // Return the offset vector for the TextBlock object.
            Vector vector = VisualTreeHelper.GetOffset(button);

            // Convert the vector to a point value.
            Point currentPoint = new Point(vector.X, vector.Y);

            return currentPoint.X;
        }

        /// <summary>
        /// Gets the hover border's X offset.
        /// </summary>
        /// <returns></returns>
        private double GetBorderVisualOffsetX()
        {
            var visualTranform = canBorder.RenderTransform;

            return visualTranform.Value.OffsetX;
        }

       
    }
}

