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
        private List<HorizontalMenuButton> buttons = new List<HorizontalMenuButton>();

        private double from = 0;

        private double selectedButtonOffset = 0;

        public HorizontalMenuBar()
        {
            InitializeComponent();

            buttons.Add(button1);
            buttons.Add(button2);
            buttons.Add(button3);
            buttons.Add(button4);
        }

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

        private void HorizontalMenuButton_Click(object sender, EventArgs e)
        {
            foreach (HorizontalMenuButton button in buttons)
            {
                button.IsSelected = false;
            }

            HorizontalMenuButton bttn = (HorizontalMenuButton)sender;

            bttn.IsSelected = true;

            var btn = sender as HorizontalMenuButton;
            double xOffset = GetVisualOffsetX(btn);
            selectedButtonOffset = xOffset;
            MoveBorder(xOffset);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            from = GetBorderVisualOffsetX();
            var btn = sender as HorizontalMenuButton;
            double xOffset = GetVisualOffsetX(btn);
            MoveBorder(xOffset);
            from = xOffset;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            MoveBorder(selectedButtonOffset);
        }

        private double GetVisualOffsetX(HorizontalMenuButton button)
        {
            // Return the offset vector for the TextBlock object.
            Vector vector = VisualTreeHelper.GetOffset(button);

            // Convert the vector to a point value.
            Point currentPoint = new Point(vector.X, vector.Y);

            return currentPoint.X;
        }

        private double GetBorderVisualOffsetX()
        {
            var visualTranform = canBorder.RenderTransform;

            return visualTranform.Value.OffsetX;
        }

       
    }
}
