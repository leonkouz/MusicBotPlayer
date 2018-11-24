using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        /// Cocnatenates an array into a string.
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
    }
}