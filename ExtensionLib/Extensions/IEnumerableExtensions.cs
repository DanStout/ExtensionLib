using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for IEnumerable&lt;T&gt;
    /// </summary>
    public static class IEnumerableExtensions
    {
        ///<summary>Get a string representing the contents of this instance</summary>
        public static string GetPrintableOutput<T>(this IEnumerable<T> enumer)
        {
            var output = new StringBuilder("[");
            //Uses the raw enumerator to check if an item is the last without having to convert the IEnumerable to a list
            using (var iter = enumer.GetEnumerator())
            {
                if (iter.MoveNext())
                {
                    T curr = iter.Current;
                    var rep = (curr is string) ? curr.Wrap('"') : curr.ToString();
                    while (iter.MoveNext())
                    {
                        output.Append(rep, ", ");
                        curr = iter.Current;
                        rep = (curr is string) ? curr.Wrap('"') : curr.ToString();
                    }
                    output.Append(rep);
                }
            }
            output.Append("]");
            return output.ToString();
        }
    }
}
