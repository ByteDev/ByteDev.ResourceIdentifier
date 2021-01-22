using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Uri" />.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Add or update a query string parameter. If the value is null the param is removed.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="key">Query string parameter name.</param>
        /// <param name="value">Query string parameter value.</param>
        /// <returns>A Uri with the added or modified query string parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="key" /> was null or empty.</exception>
        public static Uri AddOrUpdateQueryParam(this Uri source, string key, string value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key was null or empty.", nameof(key));

            var nameValues = UriQueryConverter.ToNameValueCollection(source.Query);

            if (value == null)
                nameValues.Remove(key);
            else
                nameValues.AddOrUpdate(key, value);

            return source.SetQuery(nameValues);
        }

        /// <summary>
        /// Add or update a set of query string parameters using the supplied
        /// name value collection. If a value is null the param is removed.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="newNameValues">New name value collection.</param>
        /// <returns>A new Uri instance with the added or modified query string parameters.</returns>
        public static Uri AddOrUpdateQueryParams(this Uri source, NameValueCollection newNameValues)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (newNameValues == null)
                throw new ArgumentNullException(nameof(newNameValues));

            var nameValues = UriQueryConverter.ToNameValueCollection(source.Query);

            foreach (string key in newNameValues)
            {
                var newValue = newNameValues[key];

                if (newValue == null)
                    nameValues.Remove(key);
                else
                    nameValues.AddOrUpdate(key, newValue);
            }

            return source.SetQuery(nameValues);
        }

        /// <summary>
        /// Appends the given path to any existing path in the Uri.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="path">Path to append.</param>
        /// <returns>A Uri with the appended path.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri AppendPath(this Uri source, string path)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(path))
                return source;

            while (path.StartsWith("/"))
                path = path.Substring(1);

            if (source.AbsolutePath.EndsWith("/"))
                return source.SetPath(source.AbsolutePath + path);

            return source.SetPath(source.AbsolutePath + "/" + path);
        }

        /// <summary>
        /// Returns the Uri query string as a Dictionary.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <returns>Dictionary of query string name value pairs.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static IDictionary<string, string> QueryToDictionary(this Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return UriQueryConverter.ToDictionary(source.Query);
        }

        /// <summary>
        /// Returns the Uri query string as a NameValueCollection.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <returns>NameValueCollection of query string name value pairs.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static NameValueCollection QueryToNameValueCollection(this Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return UriQueryConverter.ToNameValueCollection(source.Query);
        }
    }
}
