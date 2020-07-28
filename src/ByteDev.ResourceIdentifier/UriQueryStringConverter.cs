using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Represents a converter to convert <see cref="T:System.Collections.Specialized.NameValueCollection" /> to query string.
    /// </summary>
    public class UriQueryStringConverter
    {
        /// <summary>
        /// Converts <paramref name="nameValues" /> to a valid Uri path string.
        /// </summary>
        /// <param name="nameValues">The NameValueCollection to convert.</param>
        /// <returns>A string representing a Uri path.</returns>
        public string ConvertToQueryString(NameValueCollection nameValues)
        {
            if (nameValues == null || nameValues.Count < 1)
                return string.Empty;

            var sb = new StringBuilder();

            var items = nameValues
                .AllKeys
                .SelectMany(nameValues.GetValues, (k, v) => new { key = k, value = v });

            foreach (var item in items)
            {
                sb.Append(sb.Length == 0 ? "?" : "&");
                sb.Append(item.key + "=" + item.value);
            }

            return sb.ToString();
        }
    }
}