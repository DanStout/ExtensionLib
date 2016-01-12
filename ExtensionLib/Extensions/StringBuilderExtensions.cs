using System.Text;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for StringBuilder
    /// </summary>
    public static class StringBuilderExtensions
    {
        ///<summary>Append given items to this instance</summary>
        public static StringBuilder Append(this StringBuilder builder, params object[] items)
        {
            foreach (var item in items) builder.Append(item);
            return builder;
        }

        ///<summary>Append given items to this instance, followed by a newline</summary>
        public static StringBuilder AppendLines(this StringBuilder builder, params object[] items)
        {
            foreach (var item in items) builder.Append(item);
            return builder.AppendLine();
        }
    }
}
