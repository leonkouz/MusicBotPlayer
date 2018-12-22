using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MusicBotPlayer
{
    public class ArtistListConcaternatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var artistsList = (List<ArtistSimplified>)value;

            if (artistsList == null || artistsList.Count == 0)
            {
                return "Youtube Video";
            }

            string artistConcat = String.Empty;

            foreach(ArtistSimplified artist in artistsList)
            {
                artistConcat += artist.name + ", ";
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