using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Represents a converter for to converting Uri queries in different formats.
    /// </summary>
    public static class UriQueryConverter
    {
        /// <summary>
        /// Converts <paramref name="query" /> to a valid Uri path string.
        /// </summary>
        /// <param name="query">Query NameValueCollection to convert.</param>
        /// <returns>String representing a Uri query.</returns>
        public static string ToString(NameValueCollection query)
        {
            if (query == null || query.Count < 1)
                return string.Empty;

            var sb = new StringBuilder();

            var items = query
                .AllKeys
                .SelectMany(query.GetValues, (k, v) => new { key = k, value = v });

            foreach (var item in items)
            {
                sb.Append(sb.Length == 0 ? "?" : "&");
                sb.Append(item.key + "=" + item.value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="query" /> to a NameValueCollection.
        /// </summary>
        /// <param name="query">Query string to convert.</param>
        /// <returns>NameValueCollection representing a Uri query.</returns>
        public static NameValueCollection ToNameValueCollection(string query)
        {
            if (query == null)
                return new NameValueCollection();

            return HttpUtility.ParseQueryString(query);
        }
    }
}