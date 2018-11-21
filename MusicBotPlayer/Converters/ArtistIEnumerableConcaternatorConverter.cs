using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MusicBotPlayer
{
    public class ArtistIEnumerableConcaternatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var artistEnumerable = (IEnumerable<string>)value;

            string artistConcat = String.Empty;

            foreach(string artist in artistEnumerable)
            {
                artistConcat += artist + ", ";
            }

            // Trim the whitespaces.
            artistConcat.Trim();

            // Remove the last comma from the string.
            string trimmedString = artistConcat.Remove(artistConcat.Length - 2, 2);

            return trimmedString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}