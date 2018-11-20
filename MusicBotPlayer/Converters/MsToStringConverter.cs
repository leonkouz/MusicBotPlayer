using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MusicBotPlayer
{
    public class MsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double milliseconds = System.Convert.ToDouble(value); 
            TimeSpan time = TimeSpan.FromMilliseconds(milliseconds);
            return String.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}