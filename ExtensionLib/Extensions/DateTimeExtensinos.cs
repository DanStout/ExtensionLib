using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for DateTime
    /// </summary>
    public static class DateTimeExtensinos
    {
        ///<summary>Convert this date into an ISO 8601 date formatted string (yyyy-MM-dd) </summary>
        public static string ToIso8601(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        ///<summary>Convert this date into an integer representing the ISO8601 formatted date without dashes or time</summary>
        public static int ToIntIso8601(this DateTime date)
        {
            return Convert.ToInt32(date.ToString("yyyyMMdd"));
        }

        ///<summary>Return whether this datetime is within tolerance milliseconds of another datetime</summary>
        public static bool RoughlyEquals(this DateTime date, DateTime otherDate, int tolerance)
        {
            return ((date - otherDate).Duration() < TimeSpan.FromMilliseconds(tolerance));
        }
    }
}
