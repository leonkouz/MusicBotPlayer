using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MusicBotPlayer
{
    public class StringHelper
    {
        /// <summary>
        /// Splits a string delimited by commas to an array.
        /// </summary>
        /// <param name="str">The string to split.</param>
        /// <returns></returns>
        public static string[] StringToArray(string str)
        {
            var array = str.Split(',');
            return array;
        }

        /// <summary>
        /// Concatenates an array into a string, delimited by commas.
        /// </summary>
        /// <param name="arr">The array to concatenate.</param>
        /// <returns>A string containing all elements of the array.</returns>
        public static string ArrayToString(string[] arr)
        {
            string fullString = String.Empty;

            foreach(string str in arr)
            {
                fullString += str + ",";
            }

            return fullString.TrimEnd(',');
        }

        /// <summary>
        /// Convert an ISO 8601 Duration to a string.
        /// E.g. PT19M30S to 19:30.
        /// </summary>
        /// <param name="duration">The duration to convert.</param>
        /// <returns>A string in 24 hour time format. e.g. 19:30.</returns>
        public static string ConvertISO8601DurationToString(string duration)
        {
            return XmlConvert.ToTimeSpan(duration).ToString();
        }
    }
}