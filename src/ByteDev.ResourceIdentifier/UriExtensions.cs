using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using ByteDev.Strings;

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
        /// <param name="name">Query string parameter name.</param>
        /// <param name="value">Query string parameter value.</param>
        /// <returns>A Uri with the added or modified query string parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public static Uri AddOrUpdateQueryParam(this Uri source, string name, string value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Parameter name was null or empty.", nameof(name));

            var nameValues = UriQueryConverter.ToNameValueCollection(source.Query);

            if (value == null)
                nameValues.Remove(name);
            else
                nameValues.AddOrUpdate(name, value);

            return source.SetQuery(nameValues);
        }

        /// <summary>
        /// Add or update a set of query string parameters using the supplied
        /// name value collection. If a value is null the param is removed.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="newNameValues">New name value collection.</param>
        /// <returns>A new Uri instance with the added or modified query string parameters.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="newNameValues" /> is null.</exception>
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
        /// Add or update a set of query string parameters using the supplied
        /// name value dictionary. If a value is null the param is removed.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="newNameValues">New name value dictionary.</param>
        /// <returns>A new Uri instance with the added or modified query string parameters.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="newNameValues" /> is null.</exception>
        public static Uri AddOrUpdateQueryParams(this Uri source, IDictionary<string, string> newNameValues)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (newNameValues == null)
                throw new ArgumentNullException(nameof(newNameValues));

            var nameValues = UriQueryConverter.ToNameValueCollection(source.Query);

            foreach (string key in newNameValues.Keys)
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

            if (source.HasPath() && source.AbsolutePath.EndsWith("/"))
                return source.SetPath(source.AbsolutePath + path.EnsureEndsWith("/"));

            if (source.AbsolutePath == "/")
                return source.SetPath(source.AbsolutePath + path);

            return source.SetPath(source.AbsolutePath + "/" + path);
        }

        /// <summary>
        /// Appends the given path segments to any existing path in the Uri.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="segments">Path segments to append.</param>
        /// <returns>A Uri with the appended path segments.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri AppendPath(this Uri source, params object[] segments)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (segments == null || segments.Length == 0)
                return source;

            var sb = new StringBuilder();

            foreach (var segment in segments)
            {
                if (sb.Length > 0)
                    sb.Append("/");

                sb.Append(segment.ToString()
                    .RemoveStartsWith("/")
                    .RemoveEndsWith("/"));
            }

            return AppendPath(source, sb.ToString());
        }
    }
}
