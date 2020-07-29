using System;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Uri" />.
    /// </summary>
    public static class UriSetExtensions
    {
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

            var uriBuilder = new UriBuilder(source)
            {
                Path = path,
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

            var uriBuilder = new UriBuilder(source)
            {
                Query = query
            };

            return uriBuilder.Uri;
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

            var uriBuilder = new UriBuilder(source)
            {
                Fragment = fragment
            };

            return uriBuilder.Uri;
        }
    }
}