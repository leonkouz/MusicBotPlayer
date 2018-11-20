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
    }
}