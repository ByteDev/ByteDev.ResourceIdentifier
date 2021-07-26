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
        /// Converts <paramref name="nameValues" /> to a valid Uri path string.
        /// </summary>
        /// <param name="nameValues">Query NameValueCollection to convert.</param>
        /// <returns>String representing a Uri query.</returns>
        public static string ToString(NameValueCollection nameValues)
        {
            if (nameValues == null || nameValues.Count < 1)
                return string.Empty;

            var sb = new StringBuilder();

            foreach (string name in nameValues)
            {
                if (string.IsNullOrEmpty(name))
                    continue;

                var value = nameValues[name];

                sb.Append(sb.Length == 0 ? "?" : "&");
                sb.Append(UrlEncode(name));

                if (value != null)
                {
                    sb.Append("=");

                    if (value != string.Empty)
                    {
                        sb.Append(UrlEncode(value));
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="dictionary" /> to a valid Uri path string.
        /// </summary>
        /// <param name="dictionary">Query dictionary to convert.</param>
        /// <returns>String representing a Uri query.</returns>
        public static string ToString(IDictionary<string, string> dictionary)
        {
            if (dictionary == null || dictionary.Count < 1)
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var item in dictionary)
            {
                if (item.Key == string.Empty)
                    continue;

                sb.Append(sb.Length == 0 ? "?" : "&");
                sb.Append(UrlEncode(item.Key));

                if (item.Value != null)
                {
                    sb.Append("=");

                    if (item.Value != string.Empty)
                    {
                        sb.Append(UrlEncode(item.Value));
                    }
                }
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
                if (string.IsNullOrEmpty(name))
                    continue;

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
            return query
                .RemoveQuestionMarkPrefix()
                .Split('&');
        }

        private static string UrlEncode(string text)
        {
            return HttpUtility.UrlEncode(text);
        }
    }
}