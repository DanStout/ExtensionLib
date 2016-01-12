using System;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for double
    /// </summary>
    public static class DoubleExtensions
    {
        ///<summary>Convert these degrees to radians</summary>
        public static double ToRad(this double degrees)
        {
            return Math.PI / 180 * degrees;
        }

        ///<summary>Convert these radians to degrees</summary>
        public static double ToDegrees(this double radians)
        {
            return 180 / Math.PI * radians;
        }
    }
}
