using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for string
    /// </summary>
    public static class StringExtensions
    {
        ///<summary>Make this string into titlecase, but without any all-uppercase words</summary>
        public static string MakeNice(this string str)
        {
            return (str.HasValue()) ? Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower()) : str;
        }

        ///<summary>Returns whether this string is both non-null and non-whitespace</summary>
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        ///<summary>Return the contents of a string after the first match of a regular expression</summary>
        public static string SubstringAfter(this string str, string regex, RegexOptions? options = null)
        {
            var match = options.HasValue ? Regex.Match(str, regex, options.Value) : Regex.Match(str, regex);
            if (!match.Success) return "";
            var index = match.Index;
            index += match.Value.Length;
            return str.Substring(index);
        }

        ///<summary>Return the contents of a string up to the first match of a regular expression</summary>
        public static string SubstringBefore(this string str, string regex, RegexOptions? options = null)
        {
            var match = options.HasValue ? Regex.Match(str, regex, options.Value) : Regex.Match(str, regex);
            if (!match.Success) return str;
            var index = match.Index;
            return str.Substring(0, index);
        }

        ///<summary>Format this string with given arguments</summary>
        public static string FormatWith(this string str, params object[] formatArgs)
        {
            return string.Format(str, formatArgs);
        }

        ///<summary>Return a trimmed version of this string which is one line, no longer than the specified length, not including the given ending</summary>
        public static string NoNewLinesOrLongerThan(this string str, int maxLength, string ending)
        {
            str = str.Trim();
            bool didCut = false;
            if (str.Length > maxLength)
            {
                didCut = true;
                str = str.Substring(0, maxLength);
            }
            var firstNln = str.IndexOf('\n');
            if (firstNln > -1)
            {
                didCut = true;
                str = str.Substring(0, firstNln - 1);
            }
            if (didCut) str += ending;
            return str;
        }
    }
}
