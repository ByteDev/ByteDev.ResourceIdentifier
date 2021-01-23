using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Represents a converter for to converting Uri queries in different formats. Url encoding/decoding
    /// will be used for all parameter names and values.
    /// </summary>
    public static class UriQueryConverter
    {
        /// <summary>
        /// Converts <paramref name="value" /> to a valid Uri path string.
        /// </summary>
        /// <param name="value">Query NameValueCollection to convert.</param>
        /// <returns>String representing a Uri query.</returns>
        public static string ToString(NameValueCollection value)
        {
            if (value == null || value.Count < 1)
                return string.Empty;

            var sb = new StringBuilder();

            var items = value
                .AllKeys
                .SelectMany(value.GetValues, (k, v) => new { key = k, value = v });

            foreach (var item in items)
            {
                sb.Append(sb.Length == 0 ? "?" : "&");
                sb.Append(UrlEncode(item.key));
                sb.Append("=");
                sb.Append(UrlEncode(item.value));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value" /> to a valid Uri path string.
        /// </summary>
        /// <param name="value">Query Dictionary to convert.</param>
        /// <returns>String representing a Uri query.</returns>
        public static string ToString(IDictionary<string, string> value)
        {
            if (value == null || value.Count < 1)
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var item in value)
            {
                sb.Append(sb.Length == 0 ? "?" : "&");
                sb.Append(UrlEncode(item.Key));
                sb.Append("=");
                sb.Append(UrlEncode(item.Value));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="names" /> to a valid Uri path string.
        /// </summary>
        /// <param name="names">Collection of strings to convert.</param>
        /// <returns>String representing a Uri query.</returns>
        public static string ToString(IEnumerable<string> names)
        {
            if (names == null)
                return string.Empty;

            var query = new StringBuilder();

            foreach (var name in names.Distinct())
            {
                query.Append(query.Length == 0 ? "?" : "&");
                query.Append(UrlEncode(name));
            }

            return query.ToString();
        }

        /// <summary>
        /// Converts <paramref name="query" /> to a NameValueCollection.
        /// </summary>
        /// <param name="query">Query string to convert.</param>
        /// <returns>NameValueCollection representing a Uri query.</returns>
        public static NameValueCollection ToNameValueCollection(string query)
        {
            if (string.IsNullOrEmpty(query) || query == "?")
                return new NameValueCollection();

            var pairs = ToNameValueArray(query);

            var collection = new NameValueCollection(pairs.Length);

            foreach (var pair in pairs)
            {
                var keyValuePair = QueryKeyValuePairFactory.Create(pair);

                collection.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return collection;
        }

        /// <summary>
        /// Converts <paramref name="query" /> to a Dictionary.
        /// </summary>
        /// <param name="query">Query string to convert.</param>
        /// <returns>Dictionary representing a Uri query.</returns>
        public static IDictionary<string, string> ToDictionary(string query)
        {
            if (string.IsNullOrEmpty(query) || query == "?")
                return new Dictionary<string, string>();

            var pairs = ToNameValueArray(query);

            var dict = new Dictionary<string, string>(pairs.Length);

            foreach (var pair in pairs)
            {
                var keyValuePair = QueryKeyValuePairFactory.Create(pair);

                dict.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return dict;
        }

        private static string[] ToNameValueArray(string query)
        {
            var queryFormatted = query.RemoveQuestionMarkPrefix();

            return queryFormatted.Split('&');
        }

        private static string UrlEncode(string text)
        {
            return HttpUtility.UrlEncode(text);
        }
    }
}