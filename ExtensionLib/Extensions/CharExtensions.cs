using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for char
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Makes a character lowercase
        /// </summary>
        /// <param name="c">The character</param>
        /// <returns>Passed character in lowercase</returns>
        public static char ToLower(this char c)
        {
            return char.ToLower(c);
        }
    }
}
