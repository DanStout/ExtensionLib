using System;
using System.Net;
using System.Text;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for WebClient
    /// </summary>
    public static class WebClientExtensions
    {
        private static Common.Logging.ILog logger = Common.Logging.LogManager.GetLogger(typeof(WebClientExtensions));

        ///<summary>Download the contents of a given url with the encoding specified in the response header, or the passed default encoding if that's not possible</summary>
        public static String DownloadString(this WebClient client, string url, Encoding defaultEncoding)
        {
            var data = client.DownloadData(url);

            var contentTypeStr = client.ResponseHeaders["Content-Type"];
            var cset = "charset=";
            var csetIndex = contentTypeStr.IndexOf(cset) + cset.Length;
            var encodingStr = contentTypeStr.Substring(csetIndex);
            try
            {
                defaultEncoding = Encoding.GetEncoding(encodingStr);
            }
            catch (Exception e)
            {
                logger.Warn("Error getting default encoding", e);
            }

            return defaultEncoding.GetString(data);
        }
    }
}
