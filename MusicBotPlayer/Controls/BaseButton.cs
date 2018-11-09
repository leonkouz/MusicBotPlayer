using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicBotPlayer
{
    /// <summary>
    /// Defines the base behaviour for a button.
    /// </summary>
    public abstract class BaseButton : UserControl
    {
        /// <summary>
        /// Exposes the Click event on the button.
        /// </summary>
        public virtual event EventHandler Click;

        #region Dependency Properties

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
              typeof(BaseButton), new PropertyMetadata(null));

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
           DependencyProperty.Register("Command", typeof(ICommand), typeof(BaseButton), new UIPropertyMetadata(null));

        #endregion

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
              typeof(BaseButton), new PropertyMetadata(null));

        #endregion

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public BaseButton()
        {
            this.DataContext = this;

            IsSelected = false;
        }

        /// <summary>
        /// Exposes the click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
            Click?.Invoke(this, e);
        }
    }
}