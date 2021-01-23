using System.Collections.Generic;
using System.Web;

namespace ByteDev.ResourceIdentifier
{
    internal static class QueryKeyValuePairFactory
    {
        public static KeyValuePair<string, string> Create(string pair)
        {
            var equalsPos = pair.IndexOf('=');

            if (equalsPos >= 0)
            {
                string name = pair.Substring(0, equalsPos);
                string value = pair.Substring(equalsPos + 1);

                return new KeyValuePair<string, string>(UrlDecode(name), UrlDecode(value));
            }

            return new KeyValuePair<string, string>(UrlDecode(pair), null);
        }

        private static string UrlDecode(string text)
        {
            return HttpUtility.UrlDecode(text);
        }
    }
}