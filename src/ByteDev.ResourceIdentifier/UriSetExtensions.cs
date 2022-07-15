using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ByteDev.Strings;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Uri" />.
    /// </summary>
    public static class UriSetExtensions
    {
        private const int SchemeDefaultPort = -1; // Forces UriBuilder to use the default port for the scheme

        /// <summary>
        /// Returns a new Uri instance with the scheme set.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="scheme">Scheme value to set.</param>
        /// <returns>New Uri instance with scheme set.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="scheme" /> is null or empty.</exception>
        public static Uri SetScheme(this Uri source, string scheme)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(scheme))
                throw new ArgumentException("Scheme was null or empty.", nameof(scheme));

            var builder = new UriBuilder(source)
            {
                Scheme = scheme,
                Port = source.IsDefaultPort ? SchemeDefaultPort : source.Port
            };
            
            return builder.Uri;
        }

        /// <summary>
        /// Returns a new Uri instance with port set.
        /// </summary>
        /// <param name="source">Uri  to perform the operation on.</param>
        /// <param name="port">Port number to set.</param>
        /// <returns>New Uri instance with the port set.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="port" /> must be between 0 and 65535.</exception>
        public static Uri SetPort(this Uri source, int port)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!Tcp.IsPortValid(port))
                throw new ArgumentOutOfRangeException(nameof(port), port, $"Port must be between {Tcp.MinPort} and {Tcp.MaxPort}.");

            var builder = new UriBuilder(source)
            {
                Port = port
            };

            return builder.Uri;
        }
        
        /// <summary>
        /// Returns a new Uri instance with the port set to the scheme's default.
        /// For example for HTTP the port would be set to 80.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <returns>New Uri instance with the port set.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri SetPortDefault(this Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var builder = new UriBuilder(source)
            {
                Port = SchemeDefaultPort
            };

            return builder.Uri;
        }
        
        /// <summary>
        /// Returns a new Uri with the path set.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="path">Path value to set.</param>
        /// <returns>New Uri instance with the path set.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri SetPath(this Uri source, string path)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var pathNoSlash = path.RemoveStartsWith("/");

            var uriBuilder = new UriBuilder(source)
            {
                Path = pathNoSlash
            };

            return uriBuilder.Uri;
        }
        
        /// <summary>
        /// Returns a new Uri with the query set.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="query">Query value to set.</param>
        /// <returns>New Uri instance with the query set.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri SetQuery(this Uri source, string query)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var queryNoQMark = query.RemoveStartsWith("?");

            var uriBuilder = new UriBuilder(source)
            {
                Query = queryNoQMark
            };

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Returns a new Uri with the query set.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="queryNameValues">Query name values to set.</param>
        /// <returns>New Uri instance with the query set.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri SetQuery(this Uri source, NameValueCollection queryNameValues)
        {
            var queryString = UriQueryConverter.ToString(queryNameValues);

            return SetQuery(source, queryString);
        }

        /// <summary>
        /// Returns a new Uri with the query set.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="queryNames">Query names to set.</param>
        /// <returns>New Uri instance with the query set.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri SetQuery(this Uri source, IEnumerable<string> queryNames)
        {
            var queryString = UriQueryConverter.ToString(queryNames);

            return SetQuery(source, queryString);
        }

        /// <summary>
        /// Returns a new Uri with the fragment set.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="fragment">Fragment value to set.</param>
        /// <returns>New Uri instance with the fragment set.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri SetFragment(this Uri source, string fragment)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var fragmentNoHash = fragment.RemoveStartsWith("#");

            var uriBuilder = new UriBuilder(source)
            {
                Fragment = fragmentNoHash
            };

            return uriBuilder.Uri;
        }
    }
}